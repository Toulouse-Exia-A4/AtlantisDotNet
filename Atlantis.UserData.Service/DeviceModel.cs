using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.UserData.Service
{
    [DataContract]
    public class DeviceModel
    {
        [DataMember(Name ="deviceId")]
        public int DeviceId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public DeviceTypeModel DeviceType { get; set; }
    }
}
