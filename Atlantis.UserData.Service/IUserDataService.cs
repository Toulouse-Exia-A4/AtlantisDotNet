using Atlantis.UserData.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Atlantis.UserData.Service
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IUserDataService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IUserDataService
    {
        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "/users",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        List<User> GetAllUsers();

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "/users/{userId}",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        User GetUser(string userId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "/users/create",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        User CreateUser(string userId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "/users/remove",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        void RemoveUser(string userId);

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "/users/{userId}/devices",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        List<Device> GetUserDevices(string userId);

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "/devices",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        List<Device> GetDevices();

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "/devices/{deviceId}",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        Device GetDevice(string deviceId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "/devices",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        Device AddDevice(Device device);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "/devices/link",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        void LinkDeviceToUser(string deviceId, string userId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "/devices/register",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        void RegisterDevice(string deviceId, string deviceType, string deviceUnit = "");
    }
}
