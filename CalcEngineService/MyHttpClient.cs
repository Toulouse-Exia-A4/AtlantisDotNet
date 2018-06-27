using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CalcEngineService;

namespace CalcEngineService
{
    public class MyHttpClient
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

        /*
        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }
        */

        public static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create the calculated metric
                Calculator calc = new Calculator();
                List<CalculatedMetricsModel> calculatedMetrics = calc.generateCalculatedMetrics(); // bouchon
                
                var url = await CreateCalculatedMetric(calculatedMetrics);
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
