using Atlantis.RawMetrics.DAL;
using Atlantis.RawMetrics.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngineService
{
    public class Calculator
    {
        public double calcAverageValue(List<int> values)
        {
            return values.Average();
        }

        public double calcMedianValue(List<int> values)
        {
            values.Sort();
            var n = values.Count;
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

        public double calcHighestValue(List<int> values)
        {
            return values.Max();
        }

        public double calcLowestValue(List<int> values)
        {
            return values.Min();
        }

        public List<CalculatedMetricsModel> generateCalculatedMetrics()
        {
            RawMetricsContext context = new RawMetricsContext();
            RawMetricsDAO rawMetricsDAO = new RawMetricsDAO(context);

            // TODO retrieve Model with DAL (stamp)
            RawMetric rawMetric = new RawMetric();
            rawMetric.DeviceId = "1";
            rawMetric.Date = new DateTime(2018, 06, 27);
            rawMetric.Value = "30";
            rawMetricsDAO.Create(rawMetric);
            
            rawMetric.Date = new DateTime(2018, 06, 28);
            rawMetric.Value = "28";
            rawMetricsDAO.Create(rawMetric);
            
            // Calculation
            return Calculation(rawMetricsDAO.GetAllOrderedByDeviceId());
        }

        public List<CalculatedMetricsModel> Calculation(List<RawMetric> rawMetrics)
        {
            List<CalculatedMetricsModel> calculatedMetrics = new List<CalculatedMetricsModel>();
            CalculatedMetricsModel calculatedMetric = new CalculatedMetricsModel();
            String deviceId = rawMetrics[0].DeviceId;
            List<int> values = new List<int>(); // List of the values we want to make calculations on
            for(int i = 0 ; i < rawMetrics.Count() ; i++)
            {
                values.Add(Int32.Parse(rawMetrics[i].Value));
                i++;
                if(rawMetrics[i].DeviceId.Equals(rawMetrics[i+1].DeviceId))
                {
                    values.Add(Int32.Parse(rawMetrics[i + 1].Value));
                } else
                {
                    // do calculation
                    values.Add(Int32.Parse(rawMetrics[i + 1].Value));
                    calculatedMetric.DeviceId = rawMetrics[i + 1].DeviceId;
                    // TODO continue to fill calculatedMetric + add a way to know the date range

                }
            }
            foreach (RawMetric rawMetric in rawMetrics)
            {
                // TODO algorithm

            }
            return new List<CalculatedMetricsModel>();
        }
    }
}
