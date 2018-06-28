using Atlantis.RawMetrics.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RawMetrics.API
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IRawMetricsService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/getRawMetricsFromDevice?deviceId={deviceId}&date={date}&amount={amount}", ResponseFormat = WebMessageFormat.Json)]
        List<RawMetric> GetRawMetricsFromDevice(string deviceId, long date, int amount);
    }
}
