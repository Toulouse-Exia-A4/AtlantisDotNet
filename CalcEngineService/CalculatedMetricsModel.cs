using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngineService
{
    public class CalculatedMetricsModel
    {
        public string deviceId { get; set; }
        public string dateTimeStart { get; set; }
        public string dateTimeEnd { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }
}
