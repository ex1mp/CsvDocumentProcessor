using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvDocumentProcessor.Domain.Entities;

namespace CsvDocumentProcessor.Repository.Repositories.SalesRepository
{
    public interface ISalesRepository:IDisposable
    {
        Task<ICollection<Sales>> GetAll();

        Task<ICollection<Sales>> GetAllWithIncludeAsync();

        void AddSale(Sales sale);

        void AddSales(List<Sales> sales);

        void Update(Sales sale);
        Sales Get(int id);
    }
}
