using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository;
using CsvDocumentProcessor.Repository.Repositories.ImplementingClasses;
using CsvDocumentProcessor.Service.Containers;
using System.Collections.Generic;
using CsvDocumentProcessor.Repository.Repositories;
using CsvDocumentProcessor.Repository.Repositories.SalesRepository;
using CsvDocumentProcessor.Service.Parcers;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class SalesCreatorService
    {
        private static LockSlimContainer syncObjContainer;
        private static CsvParcer csvParcer;
        static SalesCreatorService()
        {
            csvParcer = new CsvParcer();
            syncObjContainer = new LockSlimContainer();
        }

        public void DataFromFileToDb(string filePath)
        {
            using var dbContext = new AppDbContext();
            IRepository<Client> clientRepository = new ClientRepository(dbContext, syncObjContainer.ClientsLocker);
            IRepository<Manager> managerRepository = new ManagerRepository(dbContext, syncObjContainer.ManagersLocker);
            IRepository<Product> productRepository = new ProductRepository(dbContext, syncObjContainer.ProductsLocker);
            ISalesRepository salesRepository = new SalesRepository(dbContext);
            var csvDataContainer = csvParcer.GetDataFromCsv(filePath);
            var manager = managerRepository.Get(csvParcer.GetManagerSurname(filePath));
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
