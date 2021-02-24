using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using AppDbContext appDbContext = new AppDbContext();
            //var manager = new Manager() { Name = "Jack", Surname = "Sokolov", Post = "seller" };
            //var manager1 = new Manager() { Name = "Jack", Surname = "Porovozov", Post = "seller" };
            //var cl = new Client() { Name = "Jack", Surname = "Sokol" };
            //var pr = new Product() { ProductName = "Water" };
            //appDbContext.Add(manager);
            //appDbContext.Add(manager1); appDbContext.Add(cl); appDbContext.Add(pr);
            //appDbContext.SaveChanges();
            Sales sale = new Sales() {Manager = appDbContext.Managers.FirstOrDefault(x=> x.Surname == "Sokolov"),
                Client= appDbContext.Clients.FirstOrDefault(x=>x.Surname== "Sokol"),
                Product=appDbContext.Products.FirstOrDefault(x=>x.ProductName== "Water"),SaleDate=DateTime.Now,SaleCost=3453 };
            using AppDbContext app2DbContext = new AppDbContext();
            appDbContext.Add(sale);
            appDbContext.SaveChanges();
            Console.WriteLine("Done!");
        }
    }
}
