using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CalcEngineService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Configuration;

namespace CalcEngineService
{
    public class MyHttpClient
    {
        static HttpClient client = new HttpClient();
        
        public static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URICalcAPI"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        
            try
            {
                // Create the calculated metric
                Calculator calc = new Calculator();
                var json = new JavaScriptSerializer().Serialize(await calc.generateCalculatedMetrics());
                
                /* DEBUG : what is inside the json */
                var eventLog1 = new EventLog();
                if (!System.Diagnostics.EventLog.SourceExists("MySource"))
                {
                    System.Diagnostics.EventLog.CreateEventSource(
                        "MySource", "MyNewLog");
                }
                eventLog1.Source = "MySource";
                eventLog1.Log = "MyNewLog";
                eventLog1.WriteEntry(json.ToString());

                await client.PostAsync(client.BaseAddress, new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
            }
            catch (Exception e)
            {
                var eventLog1 = new EventLog();
                if (!System.Diagnostics.EventLog.SourceExists("MySource"))
                {
                    System.Diagnostics.EventLog.CreateEventSource(
                        "MySource", "MyNewLog");
                }
                eventLog1.Source = "MySource";
                eventLog1.Log = "MyNewLog";
                eventLog1.WriteEntry(e.Message);
            }

            Console.ReadLine();
        }

    }
}
