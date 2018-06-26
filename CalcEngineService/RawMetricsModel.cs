using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngineService
{
    class RawMetricsModel
    {
        public string Id { get; set; }
        public string deviceId { get; set; }
        public string dateTime { get; set; }
        public string value { get; set; }
    }
}
