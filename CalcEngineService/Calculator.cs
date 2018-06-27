using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngineService
{
    public class Calculator
    {
        public double calcAverageValue(int[] values)
        {
            return values.Average();
        }

        public double calcMedianValue(int[] values)
        {
            Array.Sort(values);
            var n = values.Length;
            double median;
            var isOdd = n % 2 != 0;
            if (isOdd)
            {
                median = values[(n + 1) / 2 - 1];
            }
            else
            {
                median = (values[n / 2 - 1] + values[n / 2]) / 2.0d;
            }
            return median; 
        }

        public double calcHighestValue(int[] values)
        {
            return values.Max();
        }

        public double calcLowestValue(int[] values)
        {
            return values.Min();
        }

        public List<CalculatedMetricsModel> generateCalculatedMetrics()
        {
            // TODO retrieve Model with DAL (stamp)
            List<RawMetricsModel> rawMetrics = new List<RawMetricsModel>();
            RawMetricsModel rawMetric = new RawMetricsModel();
            rawMetric.Id = 1;
            rawMetric.DeviceId = 1;
            rawMetric.DateTime = 4453545;
            rawMetric.Value = 30;
            rawMetrics.Add(rawMetric);
            rawMetric.Id = 2;
            rawMetric.DeviceId = 1;
            rawMetric.DateTime = 4453645;
            rawMetric.Value = 28;
            rawMetrics.Add(rawMetric);

            // Calculation
            List<CalculatedMetricsModel> calculatedMetrics = Calculation(rawMetrics);
            
            return Calculation(rawMetrics);
        }

        public List<CalculatedMetricsModel> Calculation(List<RawMetricsModel> rawMetrics)
        {
            List<CalculatedMetricsModel> calculatedMetrics = new List<CalculatedMetricsModel>();
            foreach (RawMetricsModel rawMetric in rawMetrics)
            {
                // TODO algorithm
            }
            return new List<CalculatedMetricsModel>();
        }
    }
}
