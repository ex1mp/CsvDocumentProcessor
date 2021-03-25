using Microsoft.Win32;
using System;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class IsLaunchedService
    {
        public void SetAppStarted()
        {
            var registryKey = Registry.LocalMachine;
            var currentUserKey = Registry.CurrentUser;
            var startKey = currentUserKey.CreateSubKey("IsStartedCsvParcer");
            if (startKey.GetValue("IsStarted") == null || startKey.GetValue("IsStarted").ToString() == "False")
            {
                startKey.SetValue("IsStarted", true);
            }
            else
            {
                throw new Exception("App is started");
            }
        }
        public void SetAppStopped()
        {
            var localMachineKey = Registry.LocalMachine;
            var currentUserKey = Registry.CurrentUser;
            var startKey = currentUserKey.CreateSubKey("IsStartedCsvParcer");
            startKey.SetValue("IsStarted", false);
        }
    }
}
