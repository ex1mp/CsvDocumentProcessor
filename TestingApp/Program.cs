using CsvDocumentWebViewer.Services.Models;
using CsvDocumentWebViewer.Services.ViewsRepository;
using CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.PtoductViewRepo;
using System;
using System.Threading.Tasks;

namespace TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagerViewRepository mr = new ManagerViewRepository();
            var cl = new ManagerView() { Name = "ASers", Surname = "Vaseres", Post = "wew",ManagerId=3 };
            Console.WriteLine(mr.Exists(2));

            //foreach (var item in cl)
            //{
            //    Console.WriteLine(item.Surname);

            //}
            //Console.ReadKey();
        }
    }
}
