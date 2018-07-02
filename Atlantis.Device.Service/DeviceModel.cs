using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.Device.Service
{
    [DataContract]
    public class DeviceModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "deviceType")]
        public string DeviceType { get; set; }
    }
}