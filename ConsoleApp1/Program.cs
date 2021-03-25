using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository;
using System;
using System.Linq;

namespace DbDataFiller
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var appDbContext = new AppDbContext();
            //var manager = new Manager() { Name = "Jack", Surname = "Sokolov", Post = "seller" };
            //var manager1 = new Manager() { Name = "Jack", Surname = "Porovozov", Post = "seller" };
            //var cl = new Client() { Name = "Jack", Surname = "Sokol" };
            //var pr = new Product() { ProductName = "Water" };
            //appDbContext.Add(manager);
            //appDbContext.Add(manager1); appDbContext.Add(cl); appDbContext.Add(pr);
            //appDbContext.SaveChanges();
            var temp = new Sales() { //Manager = appDbContext.Managers.FirstOrDefault(x => x.Surname == "Sokolov"),
            //    Client = appDbContext.Clients.FirstOrDefault(x => x.Surname == "Sokol"),
            //    Product = appDbContext.Products.FirstOrDefault(x => x.ProductName == "Water"), SaleDate = DateTime.Now, SaleCost = 11111,
                SalesId = 2,
                ManagerId = 2,
                ClientId = 1,
                ProductId =1,
                SaleCost=2288
            };
            //var temp = appDbContext.Sales.FirstOrDefault(x => x.SalesId == 1);
            appDbContext.Entry(appDbContext.Sales.FirstOrDefault(x => x.SalesId == 2)).CurrentValues.SetValues(temp);
            //appDbContext.Add(sale);
            appDbContext.SaveChanges();
            Console.WriteLine("Done!");
        }
    }
}
