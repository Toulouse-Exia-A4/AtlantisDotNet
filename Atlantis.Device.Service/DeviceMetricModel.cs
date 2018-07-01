using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.Device.Service
{
    [DataContract]
    public class DeviceMetricModel
    {
        [DataMember(Name = "deviceId")]
        public string DeviceId { get; set; }

        [DataMember(Name = "deviceType")]
        public string DeviceType { get; set; }

        [DataMember(Name = "deviceUnit")]
        public string DeviceUnit { get; set; }

        [DataMember(Name = "metricValue")]
        public string MetricValue { get; set; }

        [DataMember(Name = "metricDate")]
        public long MetricDate { get; set; }
    }
}
