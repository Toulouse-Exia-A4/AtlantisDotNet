using System;
using System.Configuration;
using System.Threading;
using Atlantis.RawMetrics.DAL.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Moq.Language.Flow;
using NUnit.Framework;

namespace Atlantis.RawMetrics.DAL.Tests
{
    [TestFixture]
    public class RawMetricsDaoTests
    {
        [Test]
        public void GivenNewEntityShouldCreateInDb()
        {
            var dbMock = new Mock<IMongoDatabase>();
            var collectionMock = new Mock<IMongoCollection<RawMetric>>();
            
            dbMock.Setup(c => c.GetCollection<RawMetric>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(collectionMock.Object);

            var context = new Mock<RawMetricsContext>(dbMock.Object);

            var dao = new RawMetricsDAO(context.Object);

            var newMetric = new RawMetric { DeviceId = "XXXX", Date = DateTime.Now, Value = "12" };

            var result = dao.Create(newMetric);
            Assert.AreEqual("XXXX", result.DeviceId);
        }

        [Test]
        public void GivenIdShouldDeleteInDb()
        {
            var dbMock = new Mock<IMongoDatabase>();
            var collectionMock = new Mock<IMongoCollection<RawMetric>>();

            dbMock.Setup(c => c.GetCollection<RawMetric>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(collectionMock.Object);
            
            var context = new Mock<RawMetricsContext>(dbMock.Object);

            var dao = new RawMetricsDAO(context.Object);
            dao.Delete(ObjectId.Empty);
            collectionMock.Verify(c => c.DeleteOne(It.IsAny<FilterDefinition<RawMetric>>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void GivenIdShouldReturnObject()
        {
           
        }

        [Test]
        public void ShouldReturnAllEntities()
        {

        }

        [Test]
        public void GivenNewEntityShouldUpdateExisting()
        {

        }

        [Test]
        public void GivenDeviceIdShouldReturnAllMetrics()
        {

        }

        [Test]
        public void ShouldReturnAllDeviceMetricsOrderedByDeviceId()
        {

        }
    }
}
