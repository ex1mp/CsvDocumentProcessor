using CsvDocumentProcessor.Domain.Entities;
using System.Linq;
using System.Threading;

namespace CsvDocumentProcessor.Repository.Repositories.ImplementingClasses
{
    public class ProductRepository : IRepository<Product>
    {
        private AppDbContext dbContext;
        private ReaderWriterLockSlim productsLockSlim;
        public ProductRepository(AppDbContext dbContext, ReaderWriterLockSlim productsLockSlim)
        {
            this.dbContext = dbContext;
            this.productsLockSlim = productsLockSlim;
        }
        public void Add(ref Product product)
        {
            var temp = Get(product.ProductName);
            if (temp == null)
            {
                productsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(product.ProductName)) == null)
                    {
                        dbContext.Products.Add(product);
                        dbContext.SaveChanges();
                    }
                }
                finally
                {
                    productsLockSlim.ExitWriteLock();
                }
            }
            else
            {
                product = temp;
            }
        }
        public void Remove(Product product)
        {
            var temp = Get(product.ProductName);
            if (temp != null)
            {
                productsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(product.ProductName)) != null)
                    {
                        dbContext.Products.Remove(product);
                        dbContext.SaveChanges();
                    }
                }
                finally
                {
                    productsLockSlim.ExitWriteLock();
                }
            }
        }
        public Product Get(string productName)
        {
            return dbContext.Products.FirstOrDefault(x => x.ProductName == productName);
        }
        public void Dispose()
        {
            productsLockSlim.Dispose();
        }
    }
}
