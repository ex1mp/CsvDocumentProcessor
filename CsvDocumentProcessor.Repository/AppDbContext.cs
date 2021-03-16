using CsvDocumentProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvDocumentProcessor.Repository
{
    public class AppDbContext : DbContext
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
            optionsBuilder.UseSqlServer(@"server=HOME-PC\SQLEXPRESS;Database=CsvDocumentProcessor;Trusted_Connection=True;");
            //data source=(local)
        }
    }
}
