using CalcEngineService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlantis.RawMetrics.Service;
using Atlantis.RawMetrics.DAL.Models;
using System.Web.Script.Serialization;

namespace TestCalculationEngineService
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();

            List<RawMetric> rawMetrics = new List<RawMetric>();
            RawMetric rawMetricModel = new RawMetric();
            rawMetricModel.Date = new DateTime(2018, 06, 30).Ticks;
            rawMetricModel.DeviceId = "6";
            rawMetricModel.Value = "100";
            rawMetrics.Add(rawMetricModel);

            rawMetricModel = new RawMetric();
            rawMetricModel.Date = new DateTime(2018, 06, 30).Ticks;
            rawMetricModel.DeviceId = "6";
            rawMetricModel.Value = "85";
            rawMetrics.Add(rawMetricModel);

            rawMetricModel = new RawMetric();
            rawMetricModel.Date = new DateTime(2018, 06, 30).Ticks;
            rawMetricModel.DeviceId = "6";
            rawMetricModel.Value = "60";
            rawMetrics.Add(rawMetricModel);

            rawMetricModel = new RawMetric();
            rawMetricModel.Date = new DateTime(2018, 06, 30).Ticks;
            rawMetricModel.DeviceId = "6";
            rawMetricModel.Value = "70";
            rawMetrics.Add(rawMetricModel);

            List<CalculatedMetricsModel> calculatedMetrics = new List<CalculatedMetricsModel>();
            calculatedMetrics = calc.Calculation(rawMetrics);

            var json = new JavaScriptSerializer().Serialize(calculatedMetrics);
            Console.WriteLine(json.ToString());
        }
    }
}
