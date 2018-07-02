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

        public async Task<List<CalculatedMetricsModel>> generateCalculatedMetrics()
        {
            RawMetricsContext context = new RawMetricsContext(true);
            RawMetricsDAO rawMetricsDAO = new RawMetricsDAO(context);
            List<RawMetric> rawMetrics = await rawMetricsDAO.GetMetricsInPeriodASC(0, 636657407000000000l); //tochange
                //(new DateTime(2018, 06, 20).Ticks, new DateTime(2018, 06, 30).Ticks);

            return Calculation(rawMetrics);
        }

        public List<CalculatedMetricsModel> Calculation(List<RawMetric> rawMetrics)
        {
            List<CalculatedMetricsModel> calculatedMetrics = new List<CalculatedMetricsModel>();
            CalculatedMetricsModel calculatedMetric = new CalculatedMetricsModel();
            //String deviceId = rawMetrics[0].DeviceId;
            List<RawMetric> rawMetricsToCalculate = new List<RawMetric>(); // List of the values we want to make calculations on
            if (!(rawMetrics.Count <= 1))
            {
                for (int i = 0; i < rawMetrics.Count(); i++)
                {
                    if (i == rawMetrics.Count() - 1) // we arrived on the last metric of the list
                    {
                        
                    }
                    else
                    {
                        if (rawMetrics[i].DeviceId.Equals(rawMetrics[i + 1].DeviceId))
                        {
                            rawMetricsToCalculate.Add(rawMetrics[i]);
                        }
                        else
                        {
                            // do calculation
                            rawMetricsToCalculate.Add(rawMetrics[i]);
                            calculatedMetrics = makeRawDevicesCalculation(calculatedMetrics, rawMetricsToCalculate);
                            rawMetricsToCalculate = new List<RawMetric>();
                            // TODO continue to fill calculatedMetric + add a way to know the date range

                        }
                    }
                }
            }
            return calculatedMetrics;
        }

        public List<CalculatedMetricsModel> makeRawDevicesCalculation(List<CalculatedMetricsModel> calculatedMetrics, List<RawMetric> rawMetricsToCalculate)
        {
            // we add common values first
            CalculatedMetricsModel calculatedMetric = new CalculatedMetricsModel();
            calculatedMetric.DeviceId = rawMetricsToCalculate[0].DeviceId;
            calculatedMetric.DateTimeStart = metricsLowestDate(rawMetricsToCalculate);
            calculatedMetric.DateTimeEnd = metricsHighestDate(rawMetricsToCalculate);
            // average calculation
            calculatedMetric.Value = calcAverageValue(getRawMetricsValues(rawMetricsToCalculate));
            calculatedMetric.DataType = "Average";
            calculatedMetrics.Add(calculatedMetric);
            // median calculation
            calculatedMetric.Value = calcMedianValue(getRawMetricsValues(rawMetricsToCalculate));
            calculatedMetric.DataType = "Median";
            calculatedMetrics.Add(calculatedMetric);
            return calculatedMetrics;
        }

        public List<int> getRawMetricsValues(List<RawMetric> rawMetrics)
        {
            List<int> values = new List<int>();
            foreach (RawMetric rawMetric in rawMetrics)
            {
                values.Add(Int32.Parse(rawMetric.Value));
            }
            return values;
        }

        public long metricsLowestDate(List<RawMetric> rawMetrics)
        {
            long date = 0;
            foreach (RawMetric rawMetric in rawMetrics)
            {
                if (rawMetric.Date < date || date == 0)
                {
                    date = rawMetric.Date;
                }
            }
            return date;
        }

        public long metricsHighestDate(List<RawMetric> rawMetrics)
        {
            long date = 0;
            foreach (RawMetric rawMetric in rawMetrics)
            {
                if (rawMetric.Date > date)
                {
                    date = rawMetric.Date;
                }
            }
            return date;
        }
    }
}
