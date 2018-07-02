using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Atlantis.BackOffice.Service
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IBackOfficeService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IBackOfficeService
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/login",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        bool Login(string username, string password);

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/users",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        List<UserModel> GetUsers();

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/devices",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        List<DeviceModel> GetDevices();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/linkdevice",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        void LinkDeviceToUser(string userId, string deviceId);
    }
}
