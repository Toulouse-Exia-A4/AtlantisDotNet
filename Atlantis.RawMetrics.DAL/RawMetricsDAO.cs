using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Atlantis.RawMetrics.DAL.Models;
using MongoDB.Driver;
using System.Configuration;

namespace Atlantis.RawMetrics.DAL
{
    public class RawMetricsDAO : IMongoDataAccess<RawMetric, ObjectId>
    {
        RawMetricsContext _context;

        private readonly IMongoCollection<RawMetric> _collection;

        public RawMetricsDAO(RawMetricsContext context)
        {
            _context = context;
            _collection = _context.database.GetCollection<RawMetric>(ConfigurationManager.AppSettings["RawMetricsCollectionName"]);
        }

        public RawMetric Create(RawMetric entity)
        {
            try
            {
                _collection.InsertOne(entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(ObjectId id)
        {
            try
            {
                _collection.DeleteOne(x => x.Id.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RawMetric Get(ObjectId id)
        {
            try
            {
                return _collection.Find(x => x.Id.Equals(id)).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RawMetric> GetAll()
        {
            try
            {
                return _collection.Find(Builders<RawMetric>.Filter.Empty).ToList();
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
                var filter = Builders<RawMetric>.Filter.Eq(s => s.Id, entity.Id);
                var result = _collection.ReplaceOne(filter, entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RawMetric> GetAllMetricsForDevice(string deviceId)
        {
            try
            {
                return _collection.Find<RawMetric>(x => x.DeviceId == deviceId).ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<RawMetric> GetAllOrderedByDeviceId()
        {
            try
            {
                return _collection.Find(Builders<RawMetric>.Filter.Empty).SortBy(x => x.DeviceId).ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
