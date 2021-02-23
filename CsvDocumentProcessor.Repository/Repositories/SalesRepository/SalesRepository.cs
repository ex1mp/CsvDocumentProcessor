using CsvDocumentProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.SalesRepository
{
    public class SalesRepository : ISalesRepository
    {
        private AppDbContext dbContext;
        public SalesRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public SalesRepository()
        {
            dbContext = new AppDbContext();
        }

        public async Task<ICollection<Sales>> GetAll()
        {
            return await dbContext.Sales.ToListAsync();
        }

        public async Task<ICollection<Sales>> GetAllWithInclude()
        {
            return await dbContext.Sales
                .Include(x => x.Manager)
                .Include(x => x.Client)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public void AddSale(Sales sale)
        {
            dbContext.Sales.Add(sale);
            dbContext.SaveChanges();
        }
        public void AddSales(List<Sales> sales)
        {
            foreach (var sale in sales)
            {
                dbContext.Sales.Add(sale);
            }
            dbContext.SaveChanges();
        }
    }
}
