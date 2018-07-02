using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Atlantis.RawMetrics.DAL;
using Atlantis.RawMetrics.DAL.Models;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace Atlantis.RawMetrics.Service.Tests
{
    [TestFixture]
    public class RawMetricsServiceTests
    {
        [Test]
        public async Task GivenDeviceIdMaxDateAndResultAmountShouldReturnTwoMetrics()
        {
            var rawMetrics = Task.FromResult(new List<RawMetric>
            {
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 1).Ticks, Value = "12"},
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 2).Ticks, Value = "13"}
            });

            var mockContext = new Mock<RawMetricsContext>();
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();

            mockContext.Setup(x => x.RawMetrics).Returns(mockCollection.Object);
            var mockDao = new Mock<RawMetricsDAO>(mockContext.Object);

            mockDao.Setup(x => x.GetNDeviceMetricsPriorDate(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<int>())).Returns(rawMetrics);

            var svc = new RawMetricsService(mockDao.Object);

            var results = await svc.GetRawMetricsFromDevice("aaaa-aaaa-aaaa", new DateTime(2018, 1, 3).Ticks, 2);

            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void GivenMissingDeviceIdParameterShouldThrowException()
        {
            List<RawMetric> rawMetrics = new List<RawMetric>
            {
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 1).Ticks, Value = "12"},
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 2).Ticks, Value = "13"},
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 3).Ticks, Value = "14"},
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 4).Ticks, Value = "15"}
            };

            var mockContext = new Mock<RawMetricsContext>();
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();

            mockContext.Setup(x => x.RawMetrics).Returns(mockCollection.Object);
            var mockDao = new Mock<RawMetricsDAO>(mockContext.Object);

            mockDao.Setup(x => x.GetMetricsForDevice(It.IsAny<string>())).Returns(rawMetrics);

            var svc = new RawMetricsService(mockDao.Object);
            
            Assert.ThrowsAsync<WebFaultException<string>>(async () => await svc.GetRawMetricsFromDevice("", new DateTime(2018, 1, 3).Ticks, 2));
        }

        [Test]
        public async Task GivenAmountLowerThanOneShouldReturnEmptyList()
        {
            List<RawMetric> rawMetrics = new List<RawMetric>
            {
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 1).Ticks, Value = "12"},
            };

            var mockContext = new Mock<RawMetricsContext>();
            var mockCollection = new Mock<IMongoCollection<RawMetric>>();

            mockContext.Setup(x => x.RawMetrics).Returns(mockCollection.Object);
            var mockDao = new Mock<RawMetricsDAO>(mockContext.Object);

            mockDao.Setup(x => x.GetMetricsForDevice(It.IsAny<string>())).Returns(rawMetrics);

            var svc = new RawMetricsService(mockDao.Object);
            var results = await svc.GetRawMetricsFromDevice("aaaa-aaaa-aaaa", new DateTime(2018, 1, 3).Ticks, 0);

            Assert.AreEqual(0, results.Count);
        }
    }
}
