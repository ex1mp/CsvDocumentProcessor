using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class FileWatcherService : IDisposable
    {
        private readonly DocumentProcessorService _documentProcessorService;
        private readonly FileSystemWatcher _watcher;
        private bool _enabled = true;
        private readonly IsLaunchedService _isLaunchedService;
        public FileWatcherService()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            _isLaunchedService = new IsLaunchedService();
            _documentProcessorService = new DocumentProcessorService();
            var filePath = ConfigurationManager.AppSettings.Get("FilePath");
            _watcher = new FileSystemWatcher {Path = filePath ?? throw new InvalidOperationException(), Filter = "*.csv"};
            _watcher.Changed += Watcher_NewFileDetected;
            _watcher.Created += Watcher_NewFileDetected;
        }
        public void FileWatcherStart()
        {
            _isLaunchedService.SetAppStarted();
            _watcher.EnableRaisingEvents = true;
            _enabled = true;
            while (_enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void FileWatcherStop()
        {
            _isLaunchedService.SetAppStopped();
            _watcher.EnableRaisingEvents = false;
            _enabled = false;
        }

        private void Watcher_NewFileDetected(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Watcher_NewFileDetected");
            _documentProcessorService.DocumentProcessor();
        }
        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.Beep();
            _isLaunchedService.SetAppStopped();
        }
        public void Dispose()
        {
            _isLaunchedService.SetAppStopped();
            _watcher.Dispose();
        }

    }
}
