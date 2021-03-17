using CsvDocumentProcessor.Domain.Entities;
using System.Linq;
using System.Threading;

namespace CsvDocumentProcessor.Repository.Repositories.ImplementingClasses
{
    public class ProductRepository : IRepository<Product>
    {
        private AppDbContext _dbContext;
        private ReaderWriterLockSlim _productsLockSlim;
        public ProductRepository(AppDbContext dbContext, ReaderWriterLockSlim productsLockSlim)
        {
            this._dbContext = dbContext;
            this._productsLockSlim = productsLockSlim;
        }
        public void Add(ref Product product)
        {
            var temp = Get(product.ProductName);
            if (temp == null)
            {
                _productsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(product.ProductName)) == null)
                    {
                        _dbContext.Products.Add(product);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _productsLockSlim.ExitWriteLock();
                }
            }
            else
            {
                product = temp;
            }
        }
        public void Remove(int id)
        {
            var temp = Get(id);
            if (temp != null)
            {
                _productsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(id)) != null)
                    {
                        _dbContext.Products.Remove(temp);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _productsLockSlim.ExitWriteLock();
                }
            }
        }
        public void Update(int id, Product product)
        {
            var temp = Get(id);
            if (temp != null)
            {
                _productsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(id)) != null)
                    {
                        _dbContext.Update(product);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _productsLockSlim.ExitWriteLock();
                }
            }

        }
        public Product Get(int id)
        {
            return _dbContext.Products.FirstOrDefault(x => x.ProductId == id);
        }
        public Product Get(string productName)
        {
            return _dbContext.Products.FirstOrDefault(x => x.ProductName == productName);
        }
        public bool Exists(int id)
        {
            return _dbContext.Products.Any(e => e.ProductId == id);
        }
        public void Dispose()
        {
            _productsLockSlim.Dispose();
        }
    }
}
