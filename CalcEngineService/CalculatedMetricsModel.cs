using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngineService
{
    public class CalculatedMetricsModel
    {
        public String DeviceId { get; set; }
        public int DateTimeStart { get; set; }
        public int DateTimeEnd { get; set; }
        public double Value { get; set; }
        public string DataType { get; set; }
    }
}
