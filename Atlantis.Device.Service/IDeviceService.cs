using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Atlantis.Device.Service
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IDeviceService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IDeviceService
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/device/{id}/telemetry",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
            )]
        void AddRawMetric(string id, string date, string value);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/device",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
            )]
        DeviceModel RegisterDevice(string deviceType);
    }
}