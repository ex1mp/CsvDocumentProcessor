using CsvDocumentProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository
{
    class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Database.EnsureCreated();
        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sales> Sales { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=HOME-PC/SQLEXPRESS;Database=CsvDocumentprocessor;Trusted_Connection=True;");
        }
    }
}
