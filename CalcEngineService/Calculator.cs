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

            // If we are on monday, we look for the rawdata of the week
            List<RawMetric> rawMetrics = new List<RawMetric>();
            // Otherwise, we look for  the rawdata of the previous 24h
            DateTime dateOfTheDay = new DateTime();
            if (dateOfTheDay.DayOfWeek == DayOfWeek.Monday)
            {
                dateOfTheDay = DateTime.Now;
                rawMetrics =
                await rawMetricsDAO.GetMetricsInPeriodASC(DateTimeToUnixLong(dateOfTheDay.AddDays(-7)), DateTimeToUnixLong(dateOfTheDay));
            } else
            {
                rawMetrics =
                await rawMetricsDAO.GetMetricsInPeriodASC(DateTimeToUnixLong(dateOfTheDay.AddDays(-1)), DateTimeToUnixLong(dateOfTheDay));
            }
            return Calculation(rawMetrics);
        }

        public List<CalculatedMetricsModel> Calculation(List<RawMetric> rawMetrics)
        {
            List<CalculatedMetricsModel> calculatedMetrics = new List<CalculatedMetricsModel>();
            CalculatedMetricsModel calculatedMetric = new CalculatedMetricsModel();
            List<RawMetric> rawMetricsToCalculate = new List<RawMetric>(); // List of the values we want to make calculations on
            if (!(rawMetrics.Count <= 1))
            {
                for (int i = 0; i < rawMetrics.Count(); i++)
                {                    
                    if (i != (rawMetrics.Count() - 1)) {
                        // we also check here if we're not on the last item of the list
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
                        }
                    } else
                    {
                        // do calculation
                        rawMetricsToCalculate.Add(rawMetrics[i]);
                        calculatedMetrics = makeRawDevicesCalculation(calculatedMetrics, rawMetricsToCalculate);
                        rawMetricsToCalculate = new List<RawMetric>();
                    }
                }
            }
            return calculatedMetrics;
        }

        public List<CalculatedMetricsModel> makeRawDevicesCalculation(List<CalculatedMetricsModel> calculatedMetrics, List<RawMetric> rawMetricsToCalculate)
        {
            // average calculation
            CalculatedMetricsModel calculatedMetric = new CalculatedMetricsModel();
            calculatedMetric.DeviceId = rawMetricsToCalculate[0].DeviceId;
            calculatedMetric.DateTimeStart = metricsLowestDate(rawMetricsToCalculate);
            calculatedMetric.DateTimeEnd = metricsHighestDate(rawMetricsToCalculate);
            calculatedMetric.Value = calcAverageValue(getRawMetricsValues(rawMetricsToCalculate));
            calculatedMetric.DataType = "Average";
            calculatedMetrics.Add(calculatedMetric);
            // median calculation
            calculatedMetric = new CalculatedMetricsModel();
            calculatedMetric.DeviceId = rawMetricsToCalculate[0].DeviceId;
            calculatedMetric.DateTimeStart = metricsLowestDate(rawMetricsToCalculate);
            calculatedMetric.DateTimeEnd = metricsHighestDate(rawMetricsToCalculate);
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
        public long DateTimeToUnixLong(DateTime dt)
        {
            var timespan = (dt - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timespan.TotalMilliseconds;
        }
    }
}
