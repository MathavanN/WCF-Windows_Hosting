using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WindowsService
{
    public partial class CalculatorWindowsService : ServiceBase
    {
        ServiceHost host;
        public CalculatorWindowsService()
        {
            InitializeComponent();
        }
        private System.Threading.Thread _thread;
        protected override void OnStart(string[] args)
        {
            try
            {
                // Uncomment this line to debug...
                //System.Diagnostics.Debugger.Break();

                // Create the thread object that will do the service's work.
                _thread = new System.Threading.Thread(DoWork);

                // Start the thread.
                _thread.Start();

                // Log an event to indicate successful start.
                EventLog.WriteEntry("Successful start.", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                // Log the exception.
                _thread.Abort();
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
            
        }
        private void DoWork()
        {
            host = new ServiceHost(typeof(WcfWindowsService.Calculator));
            host.Open();
        }
        protected override void OnStop()
        {
            if(host != null)
            {
                host.Close();
                host = null;
            }
        }
    }
}
