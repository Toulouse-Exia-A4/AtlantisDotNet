using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.RawMetrics.DAL.Models
{
    public class RawMetric
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("device_id")]
        public string DeviceId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("value")]
        public string Value { get; set; }

        public RawMetric()
        {
            this.Id = ObjectId.GenerateNewId();
        }
    }
}
