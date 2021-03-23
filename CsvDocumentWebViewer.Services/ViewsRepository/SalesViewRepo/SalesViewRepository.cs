using CsvDocumentProcessor.Repository.Repositories.SalesRepository;
using CsvDocumentWebViewer.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.SalesViewRepo
{
    public class SalesViewRepository : ISalesViewRepository
    {
        private SalesMapper _salesMapper;
        public SalesViewRepository()
        {
            _salesMapper = new SalesMapper();
        }
        //tested ok
        public async Task<ICollection<SalesView>> GetAllAsync()
        {
            using SalesRepository salesRepository = new SalesRepository();
            var sales = await salesRepository.GetAllWithIncludeAsync();
            return _salesMapper.MapSales(sales);
        }
        //tested ok
        public SalesView Get(int? id)
        {
            using SalesRepository salesRepository = new SalesRepository();
            var sales = salesRepository.Get(id);
            return _salesMapper.MapSales(sales);
        }
        //tested ok
        public void Add(SalesView salesView)
        {
            using SalesRepository salesRepository = new SalesRepository();
            var sales = _salesMapper.MapSalesView(salesView);
            salesRepository.AddSale(sales);
        }
        //tested ok
        public void Update(SalesView salesView)
        {
            using SalesRepository salesRepository = new SalesRepository();
            var sales = _salesMapper.MapSalesView(salesView);
            salesRepository.Update(sales);
        }
        //tested ok
        public void Delete(int id)
        {
            using SalesRepository salesRepository = new SalesRepository();
            salesRepository.Remove(id);
        }
        //tested ok
        public bool Exists(int id)
        {
            using SalesRepository salesRepository = new SalesRepository();
            return salesRepository.Exists(id);
        }
    }
}
