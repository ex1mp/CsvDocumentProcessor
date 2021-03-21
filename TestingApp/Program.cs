using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories;
using CsvDocumentWebViewer.Services.Models;
using CsvDocumentWebViewer.Services.ViewsRepository;
using CsvDocumentWebViewer.Services.ViewsRepository.ClientViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.PtoductViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.SalesViewRepo;
using System;
using System.Threading.Tasks;

namespace TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SalesViewRepository salesViewRepository = new();
            IProductViewRepository productViewRepository = new ProductViewRepository();
            ClientViewRepository clientViewRepository = new();
            ManagerViewRepository managerViewRepository = new();
            var f = productViewRepository.GetAllAsync().Result;
            Console.WriteLine("dss");
            //Console.WriteLine(salesViewRepository.Exists(5));
            //var salV = new SalesView()
            //{
            //    Client = new ClientView() { ClientId = 1, Surname = "sds", Name = "sds" },
            //    ClientId=1,
            //    Manager = new ManagerView() { ManagerId=1,Name="df",Surname="dfd",Post="sss"},
            //    ManagerId = 1,
            //    Product = new ProductView() { ProductId=1,ProductName="dsdfs"},
            //    ProductId = 1,
            //    SaleCost = 324324,
            //    SaleDate= DateTime.Now,

            //};
            //var p = productViewRepository.Get(1);
            // var c = clientViewRepository.Get(1);
            //var m = managerViewRepository.Get(2);
            // var s = new SalesView();
            // //s.Manager = m;
            // s.SalesId = 7;
            // s.ManagerId = 1;
            // s.ClientId = 1;
            // s.ProductId = 1;
            // s.SaleCost = 345345;
            // s.SaleDate = DateTime.Now;
            // //s.Client = c;
            //// s.Product = p;
            // salesViewRepository.Update(s);


            //var item = salesViewRepository.Get(1);
            //Console.WriteLine(item.Client.Name + " " + item.Manager.Surname + " " + item.SalesId);
            //var z = salesViewRepository.GetAllAsync().Result;

            //foreach (var item in z)
            //{
            //    Console.WriteLine(item.Client.Name + " " + item.Manager.Surname + " " + item.SalesId);

            //}
            //Console.ReadKey();
        }
    }
}
