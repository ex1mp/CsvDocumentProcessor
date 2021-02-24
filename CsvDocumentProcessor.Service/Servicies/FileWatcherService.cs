using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class FileWatcherService : IDisposable
    {
        private readonly string filePath;
        private DocumentProcessorService documentProcessorService;
        private FileSystemWatcher watcher;
        private bool enabled = true;
        private IsLaunchedService isLaunchedService;
        public FileWatcherService()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            isLaunchedService = new IsLaunchedService();
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
            isLaunchedService.SetAppStarted();
            watcher.EnableRaisingEvents = true;
            enabled = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void FileWatcherStop()
        {
            isLaunchedService.SetAppStopped();
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        private void Watcher_NewFileDetected(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Watcher_NewFileDetected");
            documentProcessorService.DocumentProcessor();
        }
        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.Beep();
            isLaunchedService.SetAppStopped();
        }
        public void Dispose()
        {
            isLaunchedService.SetAppStopped();
            watcher.Dispose();
        }

    }
}
