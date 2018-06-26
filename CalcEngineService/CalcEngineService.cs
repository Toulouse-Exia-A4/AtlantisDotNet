using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngineService
{
    public partial class CalcEngineService : ServiceBase
    {
        private int eventId = 1;

        public CalcEngineService()
        {
            InitializeComponent();
            eventLog1 = new EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Calc Engine started", EventLogEntryType.Information);
            MyHttpClient.RunAsync().GetAwaiter().GetResult();
            eventLog1.WriteEntry("Calc Engine finished working", EventLogEntryType.Information);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
        }
    }
}
