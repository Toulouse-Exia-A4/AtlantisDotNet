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
        MongoClient client;
        public IMongoDatabase database;

        public RawMetricsContext()
        {
            var dbName = ConfigurationManager.AppSettings["DbName"];
            var dbUsername = ConfigurationManager.AppSettings["DbUsername"];
            var dbPassword = ConfigurationManager.AppSettings["DbPassword"];
            var dbPort = ConfigurationManager.AppSettings["DbPort"];
            var dbHost = ConfigurationManager.AppSettings["DbHost"];

            var credentials = MongoCredential.CreateCredential(dbName, dbUsername, dbPassword);

            var settings = new MongoClientSettings
            {
                Credential = credentials,
                Server = new MongoServerAddress(dbHost, Convert.ToInt32(dbPort))
            };

            client = new MongoClient(settings);
            database = client.GetDatabase(dbName);
        }

        public RawMetricsContext(IMongoDatabase db)
        {
            database = db;
        }
    }
}
