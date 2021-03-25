using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository;
using CsvDocumentProcessor.Repository.Repositories;
using CsvDocumentProcessor.Repository.Repositories.ImplementingClasses;
using CsvDocumentProcessor.Repository.Repositories.SalesRepository;
using CsvDocumentProcessor.Service.Parcers;
using System.Collections.Generic;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class SalesCreatorService
    {
        private static readonly LockSlimContainer SyncObjContainer;
        private static readonly CsvParcer CsvParcer;
        static SalesCreatorService()
        {
            CsvParcer = new CsvParcer();
            SyncObjContainer = new LockSlimContainer();
        }

        public void DataFromFileToDb(string filePath)
        {
            using var dbContext = new AppDbContext();
            using IRepository<Client> clientRepository = new ClientRepository(dbContext, SyncObjContainer.ClientsLocker);
            using IRepository<Manager> managerRepository = new ManagerRepository(dbContext, SyncObjContainer.ManagersLocker);
            using IRepository<Product> productRepository = new ProductRepository(dbContext, SyncObjContainer.ProductsLocker);
            ISalesRepository salesRepository = new SalesRepository(dbContext, SyncObjContainer.SalesLocker);
            var csvDataContainer = CsvParcer.GetDataFromCsv(filePath);
            var manager = managerRepository.Get(CsvParcer.GetManagerSurname(filePath));
            var sales = new List<Sales>();
            foreach (var item in csvDataContainer)
            {
                var sale = new Sales()
                {
                    Product = productRepository.Get(item.Product),
                    Client = clientRepository.Get(item.Client),
                    Manager = manager,
                    SaleCost = item.SaleSum,
                    SaleDate = item.SaleDate
                };
                sales.Add(sale);
            }
            salesRepository.AddSales(sales);
        }

    }
}
