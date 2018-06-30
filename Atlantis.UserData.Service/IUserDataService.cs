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
    [ServiceContract]
    public interface IUserDataService
    {
        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "/users/{userId}",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        UserModel GetUser(string userId);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "/users",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        UserModel AddUser(string userId, string firstname, string lastname);
    }
}
