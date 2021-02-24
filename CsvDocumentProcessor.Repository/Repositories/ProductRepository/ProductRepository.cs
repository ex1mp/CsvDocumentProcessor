using CsvDocumentProcessor.Domain.Entities;
using System.Linq;

namespace CsvDocumentProcessor.Repository.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private AppDbContext dbContext;
        public ProductRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public ProductRepository()
        {
            this.dbContext = new AppDbContext();
        }
        public Product FindProduct(string productName)
        {
            return dbContext.Products.FirstOrDefault(x => x.ProductName == productName);
        }

        public void AddProduct(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public void RemoveProduct(Product product)
        {
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }
    }
}
