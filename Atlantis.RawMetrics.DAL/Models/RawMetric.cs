using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.RawMetrics.DAL.Models
{
    public class RawMetric
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        [BsonElement("device_id")]
        public int DeviceId { get; set; }

        [BsonElement("date")]
        public long Date { get; set; }

        [BsonElement("value")]
        public string Value { get; set; }
    }
}
