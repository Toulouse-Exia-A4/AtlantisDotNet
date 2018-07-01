using System;
using System.ServiceModel.Web;
using Atlantis.UserData.DAL;
using Moq;
using NUnit.Framework;

namespace Atlantis.Device.Service.Tests
{
    [TestFixture]
    public class DeviceServiceTest
    {
        [Test]
        public void AddRawMetric_GivenInvalidModelShouldThrowException()
        {
            var context = new Mock<UserDataContext>();
            var svc = new Mock<DeviceService>(context.Object);

            DeviceMetricModel invalidModel = new DeviceMetricModel()
            {
                DeviceId = null
            };

            var ex = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddRawMetric(invalidModel));
        }

        [Test]
        public void AddRawMetric_GivenNewDeviceShouldAddInDbBeforeSendMetricsToQueue()
        {
            var context = new Mock<UserDataContext>();
            var deviceDao = new Mock<DeviceDAO>(context.Object);
            var deviceTypeDao = new Mock<DeviceTypeDAO>(context.Object);

            deviceDao.Setup(x => x.GetByName(It.IsAny<string>())).Returns((UserData.DAL.Device)null);
            deviceDao.Setup(x => x.Add(It.IsAny<UserData.DAL.Device>())).Returns(new UserData.DAL.Device() { DeviceId = "Test" });
            deviceTypeDao.Setup(x => x.GetByTypeName(It.IsAny<string>())).Returns(new DeviceType() { Id = 0, Type = "type" });

            var svc = new Mock<DeviceService>(context.Object);
            svc.Setup(x => x.DeviceDAO).Returns(deviceDao.Object);
            svc.Setup(x => x.DeviceTypeDAO).Returns(deviceTypeDao.Object);

            svc.Object.AddRawMetric(new DeviceMetricModel() { DeviceId = "Test", DeviceType = "type", MetricValue = "value" });
            deviceDao.Verify(d => d.Add(It.IsAny<UserData.DAL.Device>()), Times.Once());
        }

        [Test]
        public void AddRawMetric_GivenExistingDeviceShouldSendMetricsToQueue()
        {
            var context = new Mock<UserDataContext>();
            var deviceDao = new Mock<DeviceDAO>(context.Object);

            deviceDao.Setup(x => x.GetByName(It.IsAny<string>())).Returns(new UserData.DAL.Device()
            {
                DeviceId = "Test"
            });

            var svcHelper = new Mock<DeviceServiceHelpers>();

            var svc = new Mock<DeviceService>(context.Object);
            svc.Setup(x => x.DeviceDAO).Returns(deviceDao.Object);
            svc.Setup(x => x.DeviceServiceHelper).Returns(svcHelper.Object);

            svc.Object.AddRawMetric(new DeviceMetricModel() { DeviceId = "Test", DeviceType = "type", MetricValue = "value" });
            svcHelper.Verify(x => x.SendMetricToQueue(It.IsAny<DeviceMetricModel>()), Times.Once());
        }
    }
}
