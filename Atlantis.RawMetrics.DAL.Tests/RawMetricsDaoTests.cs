using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Atlantis.RawMetrics.DAL.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Moq;
using Moq.Language.Flow;
using NUnit.Framework;

namespace Atlantis.RawMetrics.DAL.Tests
{
    [TestFixture]
    public class RawMetricsDaoTests
    {
        [Test]
        public void Create_GivenValidNewEntityShouldCreateInDb()
        {
            var ctx = new Mock<RawMetricsContext>(false);
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();

            ctx.Setup(c => c.RawMetrics).Returns(mockCollection.Object);

            var dao = new RawMetricsDAO(ctx.Object);
            var result = dao.Create(new RawMetric() { DeviceId = Guid.NewGuid().ToString(), Date = DateTime.Now.Ticks, Value = "12" });

            mockCollection.Verify(c => c.InsertOne(It.IsAny<RawMetric>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void Create_GivenInvalidEntityShouldThrowException()
        {
            var ctx = new Mock<RawMetricsContext>(false);
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();

            ctx.Setup(c => c.RawMetrics).Returns(mockCollection.Object);

            var dao = new RawMetricsDAO(ctx.Object);

            var ex = Assert.Throws<Exception>(() => dao.Create(new RawMetric() { Date = DateTime.Now.Ticks, Value = "12" }));
            Assert.AreEqual("RawMetric entity integrity not respected. Please verify all your required fields.", ex.Message);
        }

        [Test]
        public void Delete_GivenIdShouldDeleteInDb()
        {
            var ctx = new Mock<RawMetricsContext>(false);
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();

            ctx.Setup(c => c.RawMetrics).Returns(mockCollection.Object);

            var dao = new RawMetricsDAO(ctx.Object);
            dao.Delete("Tot");

            mockCollection.Verify(c => c.DeleteOne(It.IsAny<FilterDefinition<RawMetric>>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void Get_GivenIdShouldReturnObject()
        {
            var ctx = new Mock<RawMetricsContext>(false);
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();

            var cursorMock = new Mock<IAsyncCursor<RawMetric>>();
            cursorMock.Setup(c => c.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            cursorMock.Setup(c => c.Current).Returns(new[] { new RawMetric() { DeviceId = "Gonzales" } });
            
            mockCollection.Setup(c => c.FindSync(It.IsAny<FilterDefinition<RawMetric>>(), It.IsAny<FindOptions<RawMetric, RawMetric>>(), It.IsAny<CancellationToken>())).Returns(cursorMock.Object);
            ctx.Setup(c => c.RawMetrics).Returns(mockCollection.Object);

            var dao = new RawMetricsDAO(ctx.Object);

            var result = dao.Get("Toto");

            mockCollection.Verify(c => c.FindSync(It.IsAny<FilterDefinition<RawMetric>>(), It.IsAny<FindOptions<RawMetric, RawMetric>>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.AreEqual("Gonzales", result.DeviceId);
        }

        [Test]
        public void Update_GivenValidNewEntityShouldUpdateExisting()
        {
            var ctx = new Mock<RawMetricsContext>(false);
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();
            string deviceId = Guid.NewGuid().ToString();

            ctx.Setup(c => c.RawMetrics).Returns(mockCollection.Object);

            var dao = new RawMetricsDAO(ctx.Object);

            var initialValue = dao.Create(new RawMetric() { DeviceId = deviceId, Date = DateTime.Now.Ticks, Value = "12" });

            var updatedValue = dao.Update(new RawMetric() { DeviceId = deviceId, Date = DateTime.Now.Ticks, Value = "16" });

            mockCollection.Verify(c => c.ReplaceOne(It.IsAny<FilterDefinition<RawMetric>>(),It.IsAny<RawMetric>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.AreNotEqual(initialValue.Value, updatedValue.Value);
        }

        [Test]
        public void Update_GivenInvalidNewEntityShouldThrowException()
        {

            var ctx = new Mock<RawMetricsContext>(false);
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();
            string deviceId = Guid.NewGuid().ToString();

            ctx.Setup(c => c.RawMetrics).Returns(mockCollection.Object);

            var dao = new RawMetricsDAO(ctx.Object);

            var initialValue = dao.Create(new RawMetric() { DeviceId = deviceId, Date = DateTime.Now.Ticks, Value = "12" });

            var ex = Assert.Throws<Exception>(() => dao.Update(new RawMetric() { Date = DateTime.Now.Ticks, Value = "16" }));
            Assert.AreEqual("RawMetric entity integrity not respected. Please verify all your required fields.", ex.Message);
        }

        [Test]
        public void GetMetricsForDevice_GivenDeviceIdShouldCallFindSync()
        {
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();
            
            var ctx = new Mock<RawMetricsContext>(false);
            ctx.Setup(c => c.RawMetrics).Returns(mockCollection.Object);

            var dao = new RawMetricsDAO(ctx.Object);

            dao.GetMetricsForDevice("toto");
            mockCollection.Verify(c => c.FindSync(It.IsAny<FilterDefinition<RawMetric>>(), It.IsAny<FindOptions<RawMetric, RawMetric>>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void GetMetricsInPeriod_GivenValidPeriodShouldReturnDevices()
        {
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();

            var ctx = new Mock<RawMetricsContext>(false);
            ctx.Setup(c => c.RawMetrics).Returns(mockCollection.Object);

            var dao = new RawMetricsDAO(ctx.Object);

            dao.GetMetricsInPeriodASC(1, 2);
            mockCollection.Verify(c => c.FindSync(It.IsAny<FilterDefinition<RawMetric>>(), It.IsAny<FindOptions<RawMetric, RawMetric>>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void GetMetricsInPeriod_GivenInvalidPeriodShouldThrowException()
        {
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();

            var ctx = new Mock<RawMetricsContext>(false);
            ctx.Setup(c => c.RawMetrics).Returns(mockCollection.Object);

            var dao = new RawMetricsDAO(ctx.Object);

            var ex = Assert.Throws<Exception>(() => dao.GetMetricsInPeriodASC(2, 1));
            Assert.AreEqual("Error in parameters: fromDate is greater than toDate.", ex.Message);
        }
    }
}
