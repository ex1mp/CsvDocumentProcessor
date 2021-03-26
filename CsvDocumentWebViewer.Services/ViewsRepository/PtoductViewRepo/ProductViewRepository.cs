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
        private readonly IMapper _mapper;
        public ProductViewRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ICollection<ProductView>> GetAllAsync()
        {
            using var productRepository = new ProductRepository();
            var products = await productRepository.GetAllAsync();
            return _mapper.Map<ICollection<ProductView>>(products);
        }
        public ICollection<ProductView> GetAll()
        {
            using var productRepository = new ProductRepository();
            var products = productRepository.GetAll();
            return _mapper.Map<ICollection<ProductView>>(products);
        }

        public ProductView Get(int? id)
        {
            using var productRepository = new ProductRepository();
            var product = productRepository.Get(id);
            return _mapper.Map<ProductView>(product);
        }

        public void Add(ProductView productView)
        {
            using var productRepository = new ProductRepository();
            var product = _mapper.Map<Product>(productView);
            productRepository.Add(ref product);

        }

        public void Update(ProductView productView)
        {
            using var productRepository = new ProductRepository();
            var product = _mapper.Map<Product>(productView);
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
