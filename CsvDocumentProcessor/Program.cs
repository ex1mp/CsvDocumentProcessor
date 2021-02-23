using CsvDocumentProcessor.Service.Servicies;
using System;

namespace CsvDocumentProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FileWatcherService fileWatcherService = new FileWatcherService();
            fileWatcherService.FileWatcherStart();
        }
    }
}
