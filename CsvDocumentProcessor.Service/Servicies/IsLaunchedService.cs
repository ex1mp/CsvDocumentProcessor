using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class IsLaunchedService
    {
        public void SetAppStarted()
        {
            RegistryKey registryKey = Registry.LocalMachine;

            RegistryKey localMachineKey = Registry.LocalMachine;
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey startKey = currentUserKey.CreateSubKey("IsStartedCsvParcer");
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
            RegistryKey localMachineKey = Registry.LocalMachine;
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey startKey = currentUserKey.CreateSubKey("IsStartedCsvParcer");
            startKey.SetValue("IsStarted", false);
        }
    }
}
