#region Using declarations
using System;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using ManagedInjector;


#endregion

//This namespace holds Add ons in this folder and is required. Do not change it. 
namespace Zweistein
{
    public partial class Diagnostics
	{
		
		public static void doSnoopWindow(Window window){
			
			Injector.LogMessage("Starting the injection process...", false);
            
            var windowHandle=PresentationSource.FromVisual(window) as HwndSource;

            Process[] localByName = Process.GetProcessesByName("Snoop");
            if (localByName != null && localByName.Length == 1)
            {

                string assemblyname = localByName[0].MainModule.FileName;
                string classname = "Snoop.SnoopUI";
                Injector.Launch(windowHandle.Handle, assemblyname,classname, "GoBabyGo");


            }
			
			
			
		}
				

        

		private static bool CheckInjectedStatus(Process process)
        {
            bool containsFile = false;
            process.Refresh();
            foreach (ProcessModule module in process.Modules)
            {
                if (module.FileName.Contains("ManagedInjector"))
                {
                    containsFile = true;
                }
            }
            if (containsFile)
            {
                Injector.LogMessage(string.Format("Successfully injected Snoop for process {0} (PID = {1})", process.ProcessName, process.Id), true);
            }
            else
            {
                Injector.LogMessage(string.Format("Failed to inject for process {0} (PID = {1})", process.ProcessName, process.Id), true);
            }

            return containsFile;
        }

        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int processId);

	}
}
