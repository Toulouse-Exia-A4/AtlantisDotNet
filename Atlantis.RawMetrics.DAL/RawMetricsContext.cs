using Atlantis.RawMetrics.DAL.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.RawMetrics.DAL
{
    public class RawMetricsContext
    {
        public virtual IMongoCollection<RawMetric> RawMetrics
        {
            get
            {
                string collectionName = ConfigurationManager.AppSettings["DbCollection"];
                if (collectionName == null)
                    throw new Exception("RawMetricsCollection name is missing in config file.");
                return database.GetCollection<RawMetric>(collectionName);
            }
        }

        MongoClient client;
        IMongoDatabase database;

        public RawMetricsContext() { }

        public RawMetricsContext(bool autoInit = true)
        {
            if (autoInit)
            {
                client = new MongoClient(ConfigurationManager.AppSettings["DbConnectionString"]);
                database = client.GetDatabase(ConfigurationManager.AppSettings["DbName"]);
            }
        }

        public RawMetricsContext(string connectionString, string dbName)
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase(dbName);
        }

        public RawMetricsContext(MongoClient client, IMongoDatabase db)
        {
            this.client = client;
            database = db;
        }
    }
}
