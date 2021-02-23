using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class FileWatcherService
    {
        private readonly string filePath;
        private DocumentProcessorService documentProcessorService;
        private FileSystemWatcher watcher;
        private bool enabled = true;
        public FileWatcherService()
        {
            documentProcessorService = new DocumentProcessorService();
            filePath = ConfigurationManager.AppSettings.Get("FilePath");
            watcher = new FileSystemWatcher();
            watcher.Path = filePath;
            watcher.Filter = "*.csv";
            watcher.Changed += Watcher_NewFileDetected;
            watcher.Created += Watcher_NewFileDetected;
            //watcher.EnableRaisingEvents = true;
        }
        public void FileWatcherStart()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void FileWatcherStop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        private void Watcher_NewFileDetected(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Watcher_NewFileDetected");
            documentProcessorService.DocumentProcessor();
        }
    }
}
