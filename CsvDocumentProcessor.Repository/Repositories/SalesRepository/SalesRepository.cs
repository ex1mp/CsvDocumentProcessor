using CsvDocumentProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.SalesRepository
{
    public class SalesRepository : ISalesRepository
    {

        public async Task<ICollection<Sales>> GetAll()
        {
            using var dbContext = new AppDbContext();
            return await dbContext.Sales.ToListAsync();
        }

        public async Task<ICollection<Sales>> GetAllWithInclude()
        {
            using var dbContext = new AppDbContext();
            return await dbContext.Sales
                .Include(x => x.Manager)
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public void AddSale(Sales sale)
        {
            using var dbContext = new AppDbContext();
            dbContext.Add(sale);
            dbContext.SaveChanges();
        }
        
    }
}
