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

        public async Task<List<RawMetricModel>> GetRawMetricsFromDevice(string deviceId, long date, int amount)
        {
            try
            {
                if (deviceId == null || deviceId.Length == 0)
                    throw new WebFaultException<string>("DeviceId cannot be empty.", HttpStatusCode.BadRequest);

                if (amount > 0)
                {
                    var results = await _dao.GetNDeviceMetricsPriorDate(deviceId, date, amount);

                    List<RawMetricModel> rawMetrics = new List<RawMetricModel>();
                    
                    if(results != null)
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
            catch (WebFaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
