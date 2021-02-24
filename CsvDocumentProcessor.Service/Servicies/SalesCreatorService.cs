using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository;
using CsvDocumentProcessor.Repository.Repositories.ClientRepository;
using CsvDocumentProcessor.Repository.Repositories.ManagerRepository;
using CsvDocumentProcessor.Repository.Repositories.ProductRepository;
using CsvDocumentProcessor.Repository.Repositories.SalesRepository;
using CsvDocumentProcessor.Service.Parcers.CsvParcer;
using System.Collections.Generic;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class SalesCreatorService
    {
        private static CsvParcer csvParcer;
        static SalesCreatorService()
        {
            csvParcer = new CsvParcer();
        }
        public void DataFromFileToDb(string filePath)
        {
            using var dbContext = new AppDbContext();
            IClientRepository clientRepository = new ClientRepository(dbContext);
            IManagerRepository managerRepository = new ManagerRepository(dbContext);
            IProductRepository productRepository = new ProductRepository(dbContext);
            ISalesRepository salesRepository = new SalesRepository(dbContext);
            var csvDataContainer = csvParcer.GetDataFromCsv(filePath);
            var manager = managerRepository.FindManager(csvParcer.GetManagerSurname(filePath));
            var sales = new List<Sales>();
            foreach (var item in csvDataContainer)
            {
                var sale = new Sales()
                {
                    Product = productRepository.FindProduct(item.Product),
                    Client = clientRepository.FindClient(item.Client),
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
