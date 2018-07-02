using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ServiceModel.Web;
using Atlantis.UserData.DAL;
using Moq;
using NUnit.Framework;

namespace Atlantis.UserData.Service.Tests
{
    [TestFixture]
    public class UserDataServiceTests
    {
        [Test]
        public void GetUser_GivenExistingUserIdWithDevicesShouldReturnUserWithDevicesList()
        {
            var dbUser = new User() { UserId = "TestUser", Firstname = "Test", Lastname = "User" };

            var userDevices = new List<Device>
            {
                new Device() { Name = "D1", DeviceType = new DeviceType() { Type = "a" } },
                new Device() { Name = "D2", DeviceType = new DeviceType() { Type = "b" } },
                new Device() { Name = "D3", DeviceType = new DeviceType() { Type = "c" } }
            };

            var context = new Mock<UserDataContext>();
            var userDao = new Mock<UserDAO>(context.Object);

            userDao.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns(dbUser);
            userDao.Setup(x => x.GetUserDevices(It.IsAny<User>())).Returns(userDevices);

            var svc = new Mock<UserDataService>(context.Object);
            svc.Setup(x => x.UserDAO).Returns(userDao.Object);

            var result = svc.Object.GetUser("TestUser");

            Assert.AreEqual("TestUser", result.UserId);
            Assert.AreEqual(3, result.Devices.Count);
        }

        [Test]
        public void GetUser_GivenExistingUserIdWithNoDevicesShouldReturnUserWithEmptyList()
        {
            var dbUser = new User() { UserId = "TestUser", Firstname = "Test", Lastname = "User" };

            var context = new Mock<UserDataContext>();
            var userDao = new Mock<UserDAO>(context.Object);

            userDao.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns(dbUser);
            userDao.Setup(x => x.GetUserDevices(It.IsAny<User>())).Returns((ICollection<Device>)null);

            var svc = new Mock<UserDataService>(context.Object);
            svc.Setup(x => x.UserDAO).Returns(userDao.Object);

            var result = svc.Object.GetUser("TestUser");

            Assert.AreEqual("TestUser", result.UserId);
            Assert.AreEqual(null, result.Devices);
        }

        [Test]
        public void GetUser_GivenNonExistingUserIdShouldReturnNull()
        {
            var context = new Mock<UserDataContext>();
            var userDao = new Mock<UserDAO>(context.Object);

            userDao.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns((User)null);

            var svc = new Mock<UserDataService>(context.Object);
            svc.Setup(x => x.UserDAO).Returns(userDao.Object);

            var result = svc.Object.GetUser("TestUser");

            Assert.AreEqual(null, result);
        }

        [Test]
        public void GetUser_GivenEmptyUserIdShouldThrowException()
        {
            var context = new Mock<UserDataContext>();

            var svc = new Mock<UserDataService>(context.Object);

            var ex = Assert.Throws<WebFaultException<string>>(() => svc.Object.GetUser(""));
            Assert.AreEqual("userId parameter is null or empty.", ex.Detail);
        }

        [Test]
        public void GetUser_GivenNullUserIdShouldThrowException()
        {
            var context = new Mock<UserDataContext>();

            var svc = new Mock<UserDataService>(context.Object);

            var ex = Assert.Throws<WebFaultException<string>>(() => svc.Object.GetUser(null));
            Assert.AreEqual("userId parameter is null or empty.", ex.Detail);
        }

        [Test]
        public void AddUser_GivenUserIdFirstnameLastnameParametersShouldAddUser()
        {
            var dbUser = new User() { UserId = "TestUser", Firstname = "Test", Lastname = "User" };

            var context = new Mock<UserDataContext>();
            var userDao = new Mock<UserDAO>(context.Object);

            userDao.Setup(x => x.Add(It.IsAny<User>())).Returns(dbUser);

            var svc = new Mock<UserDataService>(context.Object);
            svc.Setup(x => x.UserDAO).Returns(userDao.Object);

            var result = svc.Object.AddUser("TestUser", "Test", "User");

            Assert.AreEqual("TestUser", result.UserId);
            Assert.AreEqual("Test", result.Firstname);
            Assert.AreEqual("User", result.Lastname);
        }

        [Test]
        public void AddUser_GivenMissingParameterShouldThrowException()
        {
            var context = new Mock<UserDataContext>();

            var svc = new Mock<UserDataService>(context.Object);

            var case1 = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddUser(null, "test", "test"));
            var case2 = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddUser("", "test", "test"));
            var case3 = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddUser("test", null, "test"));
            var case4 = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddUser("test", "", "test"));
            var case5 = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddUser("test", "test", null));
            var case6 = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddUser("test", "test", ""));

            Assert.AreEqual("Missing parameter to add new user.", case1.Detail);
            Assert.AreEqual("Missing parameter to add new user.", case2.Detail);
            Assert.AreEqual("Missing parameter to add new user.", case3.Detail);
            Assert.AreEqual("Missing parameter to add new user.", case4.Detail);
            Assert.AreEqual("Missing parameter to add new user.", case5.Detail);
            Assert.AreEqual("Missing parameter to add new user.", case6.Detail);
        }

        [Test]
        public void AddUser_GivenExistingUserIdShouldThrowException()
        {
            var context = new Mock<UserDataContext>();

            var dao = new Mock<UserDAO>(context.Object);
            dao.Setup(x => x.Add(It.IsAny<User>())).Returns((User)null);

            var svc = new Mock<UserDataService>(context.Object);
            svc.Setup(x => x.UserDAO).Returns(dao.Object);

            var ex = Assert.Throws<WebFaultException<string>>(() => svc.Object.AddUser("test", "test", "test"));
            Assert.AreEqual("User already registered.", ex.Detail);
        }
    }
}
