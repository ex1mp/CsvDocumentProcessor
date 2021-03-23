using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories.ImplementingClasses;
using CsvDocumentWebViewer.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.PtoductViewRepo
{
    public class ProductViewRepository: IProductViewRepository
    {
        private Mapper _mappProduct;
        private Mapper _mappProductView;
        public ProductViewRepository()
        {
            var configP = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductView>());
            _mappProduct = new Mapper(configP);
            var configPV = new MapperConfiguration(cfg => cfg.CreateMap<ProductView, Product>());
            _mappProductView = new Mapper(configPV);
        }
        //tested ok
        public async Task<ICollection<ProductView>> GetAllAsync()
        {
            using ProductRepository productRepository = new ProductRepository();
            var products = await productRepository.GetAllAsync();
            return _mappProduct.Map<ICollection<ProductView>>(products);
        }
        public ICollection<ProductView> GetAll()
        {
            using ProductRepository productRepository = new ProductRepository();
            var products = productRepository.GetAll();
            return _mappProduct.Map<ICollection<ProductView>>(products);
        }
        //tested ok
        public ProductView Get(int? id)
        {
            using ProductRepository productRepository = new ProductRepository();
            var product = productRepository.Get(id);
            return _mappProduct.Map<ProductView>(product);
        }
        //tested ok
        public void Add(ProductView productView)
        {
            using ProductRepository productRepository = new ProductRepository();
            var product = _mappProductView.Map<Product>(productView);
            productRepository.Add(ref product);

        }
        //tested ok
        public void Update(ProductView productView)
        {
            using ProductRepository productRepository = new ProductRepository();
            var product = _mappProductView.Map<Product>(productView);
            productRepository.Update(product);
        }
        //tested ok
        public void Delete(int id)
        {
            using ProductRepository productRepository = new ProductRepository();
            productRepository.Remove(id);
        }
        //tested ok
        public bool Exists(int id)
        {
            using ProductRepository productRepository = new ProductRepository();
            return productRepository.Exists(id);
        }
    }
}
