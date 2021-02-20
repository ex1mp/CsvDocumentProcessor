using CsvDocumentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.ProductRepository
{
    public class ProductRepository: IProductRepository
    {
        public Product FindProduct(string productName)
        {
            using var dbContext = new AppDbContext();
            return dbContext.Products.FirstOrDefault(x => x.ProductName == productName);
        }

        public void AddProduct(Product product)
        {
            using var dbContext = new AppDbContext();
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public void RemoveProduct(Product product)
        {
            using var dbContext = new AppDbContext();
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }
    }
}
