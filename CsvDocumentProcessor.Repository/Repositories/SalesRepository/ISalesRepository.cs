using System.Collections.Generic;
using System.Threading.Tasks;
using CsvDocumentProcessor.Domain.Entities;

namespace CsvDocumentProcessor.Repository.Repositories.SalesRepository
{
    public interface ISalesRepository
    {
        Task<ICollection<Sales>> GetAll();

        Task<ICollection<Sales>> GetAllWithInclude();

        void AddSale(Sales sale);

        void AddSales(List<Sales> sales);

        void Update(int id, Sales sale);
        Sales Get(int id);
    }
}
