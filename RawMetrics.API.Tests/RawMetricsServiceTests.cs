using Atlantis.RawMetrics.DAL;
using Atlantis.RawMetrics.DAL.Models;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ServiceModel.Web;

namespace RawMetrics.API.Tests
{
    [TestFixture]
    public class RawMetricsServiceTests
    {
        [Test]
        public void GivenDeviceIdMaxDateAndResultAmountShouldReturnTwoMetrics()
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

            var results = svc.GetRawMetricsFromDevice("aaaa-aaaa-aaaa", new DateTime(2018, 1, 3).Ticks, 2);

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

            Assert.Throws<WebFaultException>(() => svc.GetRawMetricsFromDevice("", new DateTime(2018, 1, 3).Ticks, 2));
            //Assert.That(() => svc.GetRawMetricsFromDevice("", new DateTime(2018, 1, 3).Ticks, 2), Throws.Exception.With.Property("Message").EqualTo("Bad Request").And.Property("Detail").EqualTo("DeviceId cannot be empty."));
        }

        [Test]
        public void GivenAmountLowerThanOneShouldReturnEmptyList()
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
            var results = svc.GetRawMetricsFromDevice("aaaa-aaaa-aaaa", new DateTime(2018, 1, 3).Ticks, 0);

            Assert.AreEqual(0, results.Count);
        }
    }
}
