using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Atlantis.RawMetrics.DAL.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Configuration;

namespace Atlantis.RawMetrics.DAL
{
    public class RawMetricsDAO : IMongoDataAccess<RawMetric, string>
    {
        private readonly RawMetricsContext _context;

        public RawMetricsDAO()
        {
            _context = new RawMetricsContext();
        }

        public RawMetricsDAO(string connectionString, string dbName)
        {
            _context = new RawMetricsContext(connectionString, dbName);
        }

        public RawMetricsDAO(RawMetricsContext context)
        {
            _context = context;
        }

        bool Validate(RawMetric entity)
        {
            return entity.DeviceId != null && entity.DeviceId.Length > 0
                && entity.Value != null && entity.Value.Length > 0;
        }

        public RawMetric Create(RawMetric entity)
        {
            try
            {
                if (!Validate(entity))
                    throw new Exception("RawMetric entity integrity not respected. Please verify all your required fields.");

                _context.RawMetrics.InsertOne(entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                _context.RawMetrics.DeleteOne(x => x.Id.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RawMetric Get(string id)
        {
            try
            {
                return _context.RawMetrics.FindSync(x => x.Id.Equals(id)).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RawMetric Update(RawMetric entity)
        {
            try
            {
                if (!Validate(entity))
                    throw new Exception("RawMetric entity integrity not respected. Please verify all your required fields.");

                var filter = Builders<RawMetric>.Filter.Eq(s => s.Id, entity.Id);
                var result = _context.RawMetrics.ReplaceOne(filter, entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual List<RawMetric> GetMetricsForDevice(string deviceId)
        {
            try
            {
                var res = _context.RawMetrics.FindSync(x => x.DeviceId == deviceId);
                return res != null ? res.ToList() : new List<RawMetric>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RawMetric> GetMetricsInPeriodASC(long fromDate, long toDate)
        {
            try
            {
                if (fromDate > toDate)
                    throw new Exception("Error in parameters: fromDate is greater than toDate.");

                var res = _context.RawMetrics.FindSync(x => x.Date >= fromDate && x.Date <= toDate);
                return res != null ? res.ToList().OrderBy(x => x.DeviceId).ToList() : new List<RawMetric>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
