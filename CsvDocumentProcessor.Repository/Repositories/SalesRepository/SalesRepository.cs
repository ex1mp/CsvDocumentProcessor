using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CsvDocumentProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvDocumentProcessor.Repository.Repositories.SalesRepository
{
    public class SalesRepository : ISalesRepository
    {
        private AppDbContext _dbContext;
        private ReaderWriterLockSlim _salesLockSlim;
        public SalesRepository(AppDbContext dbContext, ReaderWriterLockSlim salesLockSlim)
        {
            this._dbContext = dbContext;
            this._salesLockSlim = salesLockSlim;
        }
        public SalesRepository()
        {
            _dbContext = new AppDbContext();
        }

        public async Task<ICollection<Sales>> GetAll()
        {
            return await _dbContext.Sales.ToListAsync();
        }

        public async Task<ICollection<Sales>> GetAllWithInclude()
        {
            return await _dbContext.Sales
                .Include(x => x.Manager)
                .Include(x => x.Client)
                .Include(x => x.Product)
                .ToListAsync();
        }
        public Sales Get(int id)
        {
            return _dbContext.Sales.Find(id);
        }

        public void AddSale(Sales sale)
        {
            _dbContext.Sales.Add(sale);
            _dbContext.SaveChanges();
        }
        public void AddSales(List<Sales> sales)
        {
            _dbContext.AddRange(sales);
            _dbContext.SaveChanges();
        }
        public void Remove(int id)
        {
            var temp = Get(id);
            if (temp != null)
            {
                _salesLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(id)) != null)
                    {
                        _dbContext.Sales.Remove(temp);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _salesLockSlim.ExitWriteLock();
                }
            }
        }
        public void Update(int id, Sales sale)
        {
            var temp = Get(id);
            if (temp != null)
            {
                _salesLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(id)) != null)
                    {
                        _dbContext.Update(sale);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _salesLockSlim.ExitWriteLock();
                }
            }

        }

    }
}
