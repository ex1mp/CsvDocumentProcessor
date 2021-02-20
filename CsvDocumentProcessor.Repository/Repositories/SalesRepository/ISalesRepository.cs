

using CsvDocumentProcessor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.SalesRepository
{
    public interface ISalesRepository
    {
        Task<ICollection<Sales>> GetAll();

        Task<ICollection<Sales>> GetAllWithInclude();

        void AddSale(Sales sale);
    }
}
