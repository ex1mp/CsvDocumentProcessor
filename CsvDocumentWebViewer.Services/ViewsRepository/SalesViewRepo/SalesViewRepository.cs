using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories.SalesRepository;
using CsvDocumentWebViewer.Services.ModelsView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.SalesViewRepo
{
    public class SalesViewRepository : ISalesViewRepository
    {
        IMapper _mapper;
        public SalesViewRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ICollection<SalesView>> GetAllAsync()
        {
            using var salesRepository = new SalesRepository();
            var sales = await salesRepository.GetAllWithIncludeAsync();
            return _mapper.Map<ICollection<SalesView>>(sales);
        }

        public SalesView Get(int? id)
        {
            using var salesRepository = new SalesRepository();
            var sales = salesRepository.Get(id);
            return _mapper.Map<SalesView>(sales);
        }

        public void Add(SalesView salesView)
        {
            using var salesRepository = new SalesRepository();
            var sales = _mapper.Map<Sales>(salesView);
            salesRepository.AddSale(sales);
        }

        public void Update(SalesView salesView)
        {
            using var salesRepository = new SalesRepository();
            var sales = _mapper.Map<Sales>(salesView);
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
