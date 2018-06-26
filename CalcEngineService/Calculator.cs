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
            // TODO use DAL to generate calculated metrics
            return new List<CalculatedMetricsModel>();
        }
    }
}
