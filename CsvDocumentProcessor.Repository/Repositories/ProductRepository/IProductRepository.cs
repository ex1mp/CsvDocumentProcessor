using CsvDocumentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Product FindProduct(string productName);

        void AddProduct(Product product);

        void RemoveProduct(Product product);
    }
}
