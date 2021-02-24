using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Client.WorkerWinService
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
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
        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.Beep();
            RegistryKey localMachineKey = Registry.LocalMachine;
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey startKey = currentUserKey.CreateSubKey("IsStartedCsvParcer");
            startKey.SetValue("IsStarted", false);
        }
    }
}
