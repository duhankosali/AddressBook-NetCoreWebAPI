using AddressBook.Core.Repositories;
using AddressBook.Core.Services;
using AddressBook.Core.UnitOfWorks;
using AddressBook.Repository;
using AddressBook.Repository.Repositories;
using AddressBook.Repository.UnitOfWorks;
using AddressBook.Service.Mapping;
using AddressBook.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure Services:
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Scoped
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IContactService, ContactService>();
// AddAutoMapper
builder.Services.AddAutoMapper(typeof(MapProfile));

// Connection
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); // get project name --> 'AddressBook.Repository'
    });
});


// Middlewares:
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
