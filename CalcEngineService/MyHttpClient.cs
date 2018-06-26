using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngineService
{
    class MyHttpClient
    {
        static HttpClient client = new HttpClient();
        
        static async Task<Uri> CreateCalculatedMetric(List<CalculatedMetricsModel> calculatedMetricsModels)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/calculatedMetrics", calculatedMetricsModels);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create the calculated metric
                List<CalculatedMetricsModel> calculatedMetricsModels = null; // bouchon
                
                /*
                CalculatedMetricsModel calculatedMetricsModel = new CalculatedMetricsModel
                {
                    deviceId = "1234",
                    dateTimeStart = "date",
                    dateTimeEnd = "date",
                    value = "value",
                    type = "type"
                };
                */

                var url = await CreateCalculatedMetric(calculatedMetricsModels);
                Console.WriteLine($"Created at {url}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
