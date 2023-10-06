using AddressBook.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Repository
{
    public class AppDbContext : DbContext // Entity Framework ORM
    {
        // constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // Db tables:
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // set all configurations
            base.OnModelCreating(modelBuilder);
        }
    }
}
