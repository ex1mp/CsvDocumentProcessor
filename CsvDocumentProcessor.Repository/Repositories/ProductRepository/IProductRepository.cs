using CsvDocumentProcessor.Domain.Entities;

namespace CsvDocumentProcessor.Repository.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Product FindProduct(string productName);

        void AddProduct(Product product);

        void RemoveProduct(Product product);
    }
}
