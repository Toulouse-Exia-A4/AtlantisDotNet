using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.BackOffice.Service
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Name = "userId")]
        public string UserId;

        [DataMember(Name = "firstname")]
        public string Firstname;

        [DataMember(Name = "lastname")]
        public string Lastname;

        [DataMember(Name = "devices")]
        public List<DeviceModel> Devices;
    }
}
