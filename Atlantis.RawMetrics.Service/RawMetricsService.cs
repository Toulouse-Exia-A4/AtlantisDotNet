using Atlantis.RawMetrics.DAL;
using Atlantis.RawMetrics.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

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

        public List<RawMetric> GetRawMetricsFromDevice(string deviceId, long date, int amount)
        {
            if (deviceId == null || deviceId.Length == 0)
                throw new WebFaultException<string>("DeviceId cannot be empty.", System.Net.HttpStatusCode.BadRequest);
            if (amount > 0)
            {
                var results = _dao.GetMetricsForDevice(deviceId).Where(m => m.Date < new DateTime(date).Ticks).Take(amount);
                return results.ToList();
            }
            return new List<RawMetric>();
        }
    }
}
