using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Client.WorkerWinService
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<LoggingService>();
                });

            if (isService)
            {
                 builder.UseWindowsService();
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }
    }
}
