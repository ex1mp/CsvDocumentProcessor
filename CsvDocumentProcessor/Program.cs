using CsvDocumentProcessor.Service.Servicies;

namespace CsvDocumentProcessor.Client.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Console.WriteLine("Console csvParcer started");
            var fileWatcherService = new FileWatcherService();
            fileWatcherService.FileWatcherStart();
        }
    }
}
