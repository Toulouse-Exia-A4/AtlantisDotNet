using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngineService
{
    public class RawMetricsModel
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int DateTime { get; set; }
        public int Value { get; set; }
    }
}
