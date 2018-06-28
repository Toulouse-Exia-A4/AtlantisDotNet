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
        UriTemplate = "/Users",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        List<User> GetAllUsers();
    }
}
