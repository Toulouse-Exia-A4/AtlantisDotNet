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
        //[Test]
        //public void AddRawMetric_GivenInvalidModelShouldThrowException()
        //{
        //    var context = new Mock<UserDataContext>();
        //    var svc = new Mock<DeviceService>(context.Object);
        //    svc.Setup(x => x.DeviceServiceHelper).Returns(new DeviceServiceHelpers());
        //    MetricModel invalidModel = new MetricModel()
        //    {
        //        DeviceId = null
        //    };

        //    var ex = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddRawMetric(invalidModel));
        //}

        //[Test]
        //public void AddRawMetric_GivenInexistingDeviceShouldThrowException()
        //{
        //    var context = new Mock<UserDataContext>();
        //    var deviceDao = new Mock<DeviceDAO>(context.Object);
        //    deviceDao.Setup(x => x.GetByName(It.IsAny<string>())).Returns((UserData.DAL.Device)null);

        //    var svc = new Mock<DeviceService>(context.Object);

        //    MetricModel metricModel = new MetricModel()
        //    {
        //        DeviceId = "test",
        //        Date = 1,
        //        Value = "12"
        //    };

        //    var ex = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddRawMetric(metricModel));
        //    Assert.AreEqual("Device not found.", ex.Detail);
        //}

        //[Test]
        //public void AddRawMetric_GivenExistingDeviceShouldCallSendMetricToQueue()
        //{
        //    var context = new Mock<UserDataContext>();
        //    var deviceDao = new Mock<DeviceDAO>(context.Object);
        //    deviceDao.Setup(x => x.GetByName(It.IsAny<string>())).Returns(new UserData.DAL.Device()
        //    {
        //        DeviceId = "test"
        //    });

        //    var svc = new Mock<DeviceService>(context.Object);

        //}

        [Test]
        public void RegisterDevice_GivenInvalidModelShouldThrowException()
        {

        }

        [Test]
        public void RegisterDevice_GivenNewDeviceTypeShouldAddItInDb()
        {

        }

        [Test]
        public void RegisterDevice_GivenExistingDeviceTypeShouldRetrieveItFromDb()
        {

        }

        [Test]
        public void RegisterDevice_GivenExistingDeviceShouldThrowException()
        {

        }

        [Test]
        public void RegisterDevice_GivenNewDeviceShouldAddInDb()
        {

        }


        
    }
}
