using Atlantis.RawMetrics.DAL;
using Atlantis.RawMetrics.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.RawMetrics.Service
{
    public class RawMetricsService : IRawMetricsService
    {
        private RawMetricsDAO _dao;

        public RawMetricsService()
        {
            var _context = new RawMetricsContext(true);
            _dao = new RawMetricsDAO(_context);
        }

        public RawMetricsService(RawMetricsDAO dao)
        {
            _dao = dao;
        }

        public List<RawMetricModel> GetRawMetricsFromDevice(string deviceId, string date, string amount)
        {
            try
            {
                if (deviceId.Length == 0 || date.Length == 0)
                    throw new Exception("DeviceId / Date cannot be empty.");

                int _deviceId = int.Parse(deviceId);
                long _date = long.Parse(date);
                int _amount = int.Parse(amount);

                if (_amount > 0)
                {
                    var results = _dao.GetNDeviceMetricsPriorDate(_deviceId, _date, _amount);

                    List<RawMetricModel> rawMetrics = new List<RawMetricModel>();

                    if (results != null)
                    {
                        foreach (var result in results)
                        {
                            rawMetrics.Add(new RawMetricModel() { DeviceId = result.DeviceId, Date = result.Date, Value = result.Value });
                        }
                    }

                    return rawMetrics;
                }

                return new List<RawMetricModel>();
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
