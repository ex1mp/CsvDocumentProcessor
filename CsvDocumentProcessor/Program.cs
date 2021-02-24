using CsvDocumentProcessor.Service.Servicies;
using System;

namespace CsvDocumentProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console csvParcer started");
            FileWatcherService fileWatcherService = new FileWatcherService();
            fileWatcherService.FileWatcherStart();
        }
    }
}
