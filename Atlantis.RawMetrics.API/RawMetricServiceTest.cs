using Atlantis.RawMetrics.DAL;
using Atlantis.RawMetrics.DAL.Models;
using MongoDB.Driver;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atlantis.RawMetrics.API
{
    [TestFixture]
    public class RawMetricServiceTest
    {
        [Test]
        public void GivenDeviceIdDateAndAmountShouldReturnMetrics()
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

            var jsonResults = svc.GetRawMetricsFromDevice("aaaa-aaaa-aaaa", new DateTime(2018, 1, 3).Ticks, 2);
            var results = JsonConvert.DeserializeObject<List<RawMetric>>(jsonResults);
            Assert.AreEqual(2, results.Count);
        }
    }
}