using System.Collections.Generic;
using System.Linq;
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
            _salesLockSlim = new ReaderWriterLockSlim();
        }

        public async Task<ICollection<Sales>> GetAll()
        {
            return await _dbContext.Sales.ToListAsync();
        }

        public async Task<ICollection<Sales>> GetAllWithIncludeAsync()
        {
            return await _dbContext.Sales
                .Include(x => x.Manager)
                .Include(x => x.Client)
                .Include(x => x.Product)
                .ToListAsync();
        }
        public Sales Get(int? id)
        {

            return _dbContext.Sales
                .Include(x => x.Manager)
                .Include(x => x.Client)
                .Include(x => x.Product).
                FirstOrDefault(x => x.SalesId == id);
        }
        public Sales Get(int id)
        {
            
            return _dbContext.Sales
                .Include(x => x.Manager)
                .Include(x => x.Client)
                .Include(x => x.Product).
                FirstOrDefault(x => x.SalesId == id);
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
        public void Update(Sales sale)
        {
            var temp = Get(sale.SalesId);
            if (temp != null)
            {
                _salesLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(sale.SalesId)) != null)
                    {
                        _dbContext.Entry(temp).CurrentValues.SetValues(sale);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _salesLockSlim.ExitWriteLock();
                }
            }

        }
        public bool Exists(int id)
        {
            return _dbContext.Sales.Any(e => e.SalesId == id);
        }
        public void Dispose()
        {
            _salesLockSlim.Dispose();
        }
    }
}
