using CsvDocumentProcessor.Service.Servicies;

namespace CsvDocumentProcessor.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Console csvParcer started");
            FileWatcherService fileWatcherService = new FileWatcherService();
            fileWatcherService.FileWatcherStart();
        }
    }
}
