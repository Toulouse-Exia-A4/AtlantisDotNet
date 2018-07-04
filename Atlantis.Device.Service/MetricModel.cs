using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.Device.Service
{
    [DataContract]
    public class MetricModel
    {
        [DataMember(Name = "deviceId")]
        public int DeviceId { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
