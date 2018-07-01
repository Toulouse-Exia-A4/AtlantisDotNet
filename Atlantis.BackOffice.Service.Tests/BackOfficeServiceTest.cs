using System;
using System.Collections.Generic;
using System.ServiceModel.Web;
using Atlantis.UserData.DAL;
using Moq;
using NUnit.Framework;

namespace Atlantis.BackOffice.Service.Tests
{
    [TestFixture]
    public class BackOfficeServiceTest
    {
        [Test]
        public void GetDevices_ShouldReturnDeviceModelList()
        {
            List<Device> devices = new List<Device>
            {
                new Device() {DeviceId = "Test" },
                new Device() {DeviceId = "Test1" },
                new Device() {DeviceId = "Test2" }
            };

            var context = new Mock<UserDataContext>();
            var deviceDao = new Mock<DeviceDAO>(context.Object);
            deviceDao.Setup(x => x.All()).Returns(devices);

            var svc = new Mock<BackOfficeService>(context.Object);
            svc.Setup(x => x.DeviceDAO).Returns(deviceDao.Object);

            var results = svc.Object.GetDevices();

            Assert.AreEqual(devices.Count, results.Count);
        }

        [Test]
        public void GetUsers_ShouldReturnUserModelList()
        {
            List<User> users = new List<User>
            {
                new User() {UserId = "Test" },
                new User() {UserId = "Test1" },
                new User() {UserId = "Test2" }
            };

            var context = new Mock<UserDataContext>();
            var userDao = new Mock<UserDAO>(context.Object);
            userDao.Setup(x => x.All()).Returns(users);

            var svc = new Mock<BackOfficeService>(context.Object);
            svc.Setup(x => x.UserDAO).Returns(userDao.Object);
            var results = svc.Object.GetUsers();

            Assert.AreEqual(users.Count, results.Count);
        }

        [Test]
        public void LinkDeviceToUser_ShouldCallDaoAddDeviceOwner()
        {
            var context = new Mock<UserDataContext>();
            var deviceDao = new Mock<DeviceDAO>(context.Object);
            deviceDao.Setup(x => x.AddDeviceOwner(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var svc = new Mock<BackOfficeService>(context.Object);
            svc.Setup(x => x.DeviceDAO).Returns(deviceDao.Object);

            svc.Object.LinkDeviceToUser("test", "test");

            deviceDao.Verify(x => x.AddDeviceOwner(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void LinkDeviceToUser_GivenMissingUserIdShouldThrowException()
        {
            var context = new Mock<UserDataContext>();
            var svc = new Mock<BackOfficeService>(context.Object);
            Assert.Throws<WebFaultException<string>>(() => svc.Object.LinkDeviceToUser(null, "test"));
        }

        [Test]
        public void LinkDeviceToUser_GivenMissingDeviceIdShouldThrowException()
        {
            var context = new Mock<UserDataContext>();
            var svc = new Mock<BackOfficeService>(context.Object);
            Assert.Throws<WebFaultException<string>>(() => svc.Object.LinkDeviceToUser("test", null));
        }

        [Test]
        public void Login_GivenWrongUsernameShouldThrowException()
        {
            var context = new Mock<UserDataContext>();
            var adminDao = new Mock<AdminDAO>(context.Object);

            adminDao.Setup(x => x.Find(It.IsAny<string>())).Returns((Admin)null);

            var svc = new Mock<BackOfficeService>(context.Object);
            svc.Setup(x => x.AdminDAO).Returns(adminDao.Object);

            var ex = Assert.Throws<WebFaultException<string>>(() => svc.Object.Login("test", "test"));
            Assert.AreEqual("Incorrect username.", ex.Detail);
        }

        [Test]
        public void Login_GivenWrongPasswordShouldReturnFalse()
        {
            var context = new Mock<UserDataContext>();
            var adminDao = new Mock<AdminDAO>(context.Object);

            adminDao.Setup(x => x.Find(It.IsAny<string>())).Returns(new Admin() {AdminId = "test", Password = "test" });

            var svc = new Mock<BackOfficeService>(context.Object);
            svc.Setup(x => x.AdminDAO).Returns(adminDao.Object);

            bool result = svc.Object.Login("test", "test");
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Login_GivenCorrectUsernamePasswordShouldReturnTrue()
        {
            var context = new Mock<UserDataContext>();
            var adminDao = new Mock<AdminDAO>(context.Object);

            adminDao.Setup(x => x.Find(It.IsAny<string>())).Returns(new Admin() { AdminId = "test", Password = BackOfficeHelper.GetMD5Hash("test") });

            var svc = new Mock<BackOfficeService>(context.Object);
            svc.Setup(x => x.AdminDAO).Returns(adminDao.Object);

            bool result = svc.Object.Login("test", "test");
            Assert.AreEqual(true, result);
        }
    }
}
