using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.RawMetrics.Service
{
    [DataContract]
    public class RawMetricModel
    {
        [DataMember(Name = "deviceId")]
        public string DeviceId { get; set; }

        [DataMember(Name = "date")]
        public long Date { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
