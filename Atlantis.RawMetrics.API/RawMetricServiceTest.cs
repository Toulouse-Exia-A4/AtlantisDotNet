using Atlantis.RawMetrics.DAL;
using Atlantis.RawMetrics.DAL.Models;
using Moq;
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
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 1), Value = "12"},
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 2), Value = "13"},
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 3), Value = "14"},
                new RawMetric() { DeviceId = "aaaa-aaaa-aaaa", Date = new DateTime(2018, 1, 4), Value = "15"}
            };

            var mockContext = new Mock<RawMetricsContext>();
            var mockDao = new Mock<RawMetricsDAO>(mockContext.Object);

            mockDao.Setup(x => x.GetAllMetricsForDevice(It.IsAny<string>())).Returns(rawMetrics);

            var svc = new RawMetricsService(mockDao.Object);

            var results = svc.GetRawMetricsFromDevice("aaaa-aaaa-aaaa", new DateTime(2018, 1, 3).Ticks, 2);

            Assert.AreEqual(3, results.Length);
        }
    }
}