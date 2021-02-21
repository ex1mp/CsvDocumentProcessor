using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories.ClientRepository;
using CsvDocumentProcessor.Repository.Repositories.ManagerRepository;
using CsvDocumentProcessor.Repository.Repositories.ProductRepository;
using CsvDocumentProcessor.Service.Parcers.CsvParcer;
using System.Collections.Generic;
using System.Linq;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class SalesCreatorService
    {
        private static IClientRepository clientRepository;
        private static IManagerRepository managerRepository;
        private static IProductRepository productRepository;
        private static CsvParcer csvParcer;
        static SalesCreatorService()
        {
            clientRepository = new ClientRepository();
            managerRepository = new ManagerRepository();
            productRepository = new ProductRepository();
            csvParcer = new CsvParcer();
        }
        public List<Sales> GetSales(string filePath)
        {
            var csvDataContainer = csvParcer.GetDataFromCsv(filePath);
            var manager = managerRepository.FindManager(csvParcer.GetManagerSurname(filePath));
            var sales = new List<Sales>();
            var clients = GetAllClients(csvDataContainer);
            var products = GetAllProducts(csvDataContainer);
            foreach (var item in csvDataContainer)
            {
                var sale = new Sales()
                {
                    Product = products[item.Product],
                    Customer = clients[item.Client],
                    Manager = manager,
                    SaleCost = item.SaleSum,
                    SaleDate = item.SaleDate
                };
                sales.Add(sale);

            }
            return sales;
        }
        private Dictionary<string, Client> GetAllClients(List<CsvDataContainer> csvDataContainer)
        {
            var clientsSurnames = (from csvData in csvDataContainer select csvData.Client).Distinct();
            var clients = new Dictionary<string, Client>();
            foreach (var clientSurname in clientsSurnames)
            {
                clients.Add(clientSurname, clientRepository.FindClient(clientSurname));
            }
            return clients;
        }

        private Dictionary<string, Product> GetAllProducts(List<CsvDataContainer> csvDataContainer)
        {
            var productNames = (from csvData in csvDataContainer select csvData.Product).Distinct();
            var products = new Dictionary<string, Product>();
            foreach (var productName in productNames)
            {
                products.Add(productName, productRepository.FindProduct(productName));
            }
            return products;
        }
    }
}
