using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories.ImplementingClasses;
using CsvDocumentWebViewer.Services.ModelsView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.PtoductViewRepo
{
    public class ProductViewRepository : IProductViewRepository
    {
        private readonly Mapper _mappProduct;
        private readonly Mapper _mappProductView;
        public ProductViewRepository()
        {
            var configP = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductView>());
            _mappProduct = new Mapper(configP);
            var configPv = new MapperConfiguration(cfg => cfg.CreateMap<ProductView, Product>());
            _mappProductView = new Mapper(configPv);
        }
    
        public async Task<ICollection<ProductView>> GetAllAsync()
        {
            using var productRepository = new ProductRepository();
            var products = await productRepository.GetAllAsync();
            return _mappProduct.Map<ICollection<ProductView>>(products);
        }
        public ICollection<ProductView> GetAll()
        {
            using var productRepository = new ProductRepository();
            var products = productRepository.GetAll();
            return _mappProduct.Map<ICollection<ProductView>>(products);
        }
      
        public ProductView Get(int? id)
        {
            using var productRepository = new ProductRepository();
            var product = productRepository.Get(id);
            return _mappProduct.Map<ProductView>(product);
        }
      
        public void Add(ProductView productView)
        {
            using var productRepository = new ProductRepository();
            var product = _mappProductView.Map<Product>(productView);
            productRepository.Add(ref product);

        }
       
        public void Update(ProductView productView)
        {
            using var productRepository = new ProductRepository();
            var product = _mappProductView.Map<Product>(productView);
            productRepository.Update(product);
        }
      
        public void Delete(int id)
        {
            using var productRepository = new ProductRepository();
            productRepository.Remove(id);
        }
       
        public bool Exists(int id)
        {
            using var productRepository = new ProductRepository();
            return productRepository.Exists(id);
        }
    }
}
