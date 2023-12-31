using AddressBook.Core.Repositories;
using AddressBook.Core.Services;
using AddressBook.Core.UnitOfWorks;
using AddressBook.Repository;
using AddressBook.Repository.Repositories;
using AddressBook.Repository.UnitOfWorks;
using AddressBook.Service.Mapping;
using AddressBook.Service.Services;
using AddressBook.Service.Validations;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure Services:
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AddFluentValidation
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ContactDtoValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UpdateContactDtoValidator>());

// AddScoped
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IContactService, ContactService>();

// AddAutoMapper
builder.Services.AddAutoMapper(typeof(MapProfile));

if (Environment.GetEnvironmentVariable("DOCKER_ENVIRONMENT") == "Docker") // for docker and aws 
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(80);
    });
}

// AddDbContext
string connectionString = Environment.GetEnvironmentVariable("POSTGRESQL_CONNECTION_STRING"); // Open CMD --> setx POSTGRESQL_CONNECTION_STRING "YOUR_CONNECTION_STRING" (for windows)
                                                                                             // Open Console --> export POSTGRESQL_CONNECTION_STRING="YOUR_CONNECTION_STRING" (for linux and mac)
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseNpgsql(connectionString, option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); // get project name --> 'AddressBook.Repository'
    });
});


// ---------------------------

// Middlewares:
var app = builder.Build();

// Swagger UI is on in both Production and Development modes
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection(); --> HTTPS 'i engellemek i�in.

app.UseAuthorization();

app.MapControllers();

app.Run();
