using CsvDocumentProcessor.Service.Servicies;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Client.WorkerWinService
{
    public class CsvWatchService : IHostedService, IDisposable
    {
        private FileWatcherService _fileWatcher;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Startup code
            _fileWatcher = new FileWatcherService();
            _fileWatcher.FileWatcherStart();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //Stop timers, services
            _fileWatcher.FileWatcherStop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _fileWatcher.FileWatcherStop();
            // dispose of non-managed resources
        }
    }
}
