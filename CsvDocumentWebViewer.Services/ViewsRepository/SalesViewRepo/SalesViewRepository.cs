using CsvDocumentProcessor.Repository.Repositories.SalesRepository;
using CsvDocumentWebViewer.Services.ModelsView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.SalesViewRepo
{
    public class SalesViewRepository : ISalesViewRepository
    {
        private readonly SalesMapper _salesMapper;
        public SalesViewRepository()
        {
            _salesMapper = new SalesMapper();
        }

        public async Task<ICollection<SalesView>> GetAllAsync()
        {
            using var salesRepository = new SalesRepository();
            var sales = await salesRepository.GetAllWithIncludeAsync();
            return _salesMapper.MapSales(sales);
        }

        public SalesView Get(int? id)
        {
            using var salesRepository = new SalesRepository();
            var sales = salesRepository.Get(id);
            return _salesMapper.MapSales(sales);
        }

        public void Add(SalesView salesView)
        {
            using var salesRepository = new SalesRepository();
            var sales = _salesMapper.MapSalesView(salesView);
            salesRepository.AddSale(sales);
        }

        public void Update(SalesView salesView)
        {
            using var salesRepository = new SalesRepository();
            var sales = _salesMapper.MapSalesView(salesView);
            salesRepository.Update(sales);
        }

        public void Delete(int id)
        {
            using var salesRepository = new SalesRepository();
            salesRepository.Remove(id);
        }

        public bool Exists(int id)
        {
            using var salesRepository = new SalesRepository();
            return salesRepository.Exists(id);
        }
    }
}
