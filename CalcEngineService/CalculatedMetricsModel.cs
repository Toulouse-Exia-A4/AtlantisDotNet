using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngineService
{
    public class CalculatedMetricsModel
    {
        public int DeviceId { get; set; }
        public long DateTimeStart { get; set; }
        public long DateTimeEnd { get; set; }
        public double Value { get; set; }
        public string DataType { get; set; }
    }
}
