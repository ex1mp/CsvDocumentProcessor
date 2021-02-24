using CsvDocumentProcessor.Service.Servicies;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Client.WorkerWinService
{
    public class LoggingService : IHostedService, IDisposable
    {
        FileWatcherService fileWatcher;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Startup code
            fileWatcher = new FileWatcherService();
            fileWatcher.FileWatcherStart();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop timers, services
            fileWatcher.FileWatcherStop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            fileWatcher.FileWatcherStop();
            // dispose of non-managed resources
        }
    }
}
