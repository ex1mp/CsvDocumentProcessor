using CsvDocumentProcessor.Service.Servicies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Client.WindowsService
{
    partial class CsvDocumentProcessorWindowsService : ServiceBase
    {
        FileWatcherService fileWatcherService;
        public CsvDocumentProcessorWindowsService()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            var fileWatcherService = new FileWatcherService();

            fileWatcherService.FileWatcherStart();
            //Thread fileWatcherServiceThread = new Thread(new ThreadStart(fileWatcherService.FileWatcherStart));
            //fileWatcherServiceThread.Start();
        }

        protected override void OnStop()
        {
            fileWatcherService.FileWatcherStop();
        }
    }
}
