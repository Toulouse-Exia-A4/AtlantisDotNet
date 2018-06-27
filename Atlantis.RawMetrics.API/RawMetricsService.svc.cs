using Atlantis.RawMetrics.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Atlantis.RawMetrics.API
{
    public class RawMetricsService : IRawMetricsService
    {
        private RawMetricsContext _context;
        private RawMetricsDAO _dao;

        public RawMetricsService()
        {
            _context = new RawMetricsContext();
            _dao = new RawMetricsDAO(_context);
        }

        public RawMetricsService(RawMetricsDAO dao)
        {
            _dao = dao;
        }

        public string GetRawMetricsFromDevice(string deviceId, long date, int amount)
        {
            var results = _dao.GetAllMetricsForDevice(deviceId).Where(m => m.Date < new DateTime(date)).Take(amount);

            return JsonConvert.SerializeObject(results);
        }
    }
}
