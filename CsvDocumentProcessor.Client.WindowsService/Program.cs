using System;
using System.ServiceProcess;

namespace CsvDocumentProcessor.Client.WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {

            var service = new CsvDocumentProcessorWindowsService();
            service.onDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

        }
    }
}
