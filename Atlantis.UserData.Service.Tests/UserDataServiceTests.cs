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
                new Device() { DeviceId = "D1", DeviceType = new DeviceType() { Type = "a" } },
                new Device() { DeviceId = "D2", DeviceType = new DeviceType() { Type = "b" } },
                new Device() { DeviceId = "D3", DeviceType = new DeviceType() { Type = "c" } }
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

        //[Test]
        //public void AddDevice_GivenNewDeviceShouldCallAddInDeviceDao()
        //{
        //    Device newDevice = new Device() { DeviceId = "Test" };

        //    var mockContext = new Mock<UserDataContext>();
        //    var mockDao = new Mock<DeviceDAO>(mockContext.Object);
        //    mockDao.Setup(x => x.Add(It.IsAny<Device>())).Returns(newDevice);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.DeviceDAO).Returns(mockDao.Object);

        //    svc.Object.AddDevice(newDevice);
        //    mockDao.Verify(x => x.Add(It.IsAny<Device>()), Times.Once());
        //}

        //[Test]
        //public void CreateUser_GivenNewUserShouldCallAddInUserDao()
        //{
        //    User newUser = new User() { UserId = "Test" };

        //    var mockContext = new Mock<UserDataContext>();
        //    var mockDao = new Mock<UserDAO>(mockContext.Object);
        //    mockDao.Setup(x => x.Add(It.IsAny<User>())).Returns(newUser);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.UserDAO).Returns(mockDao.Object);

        //    var created = svc.Object.CreateUser("Test");
        //    mockDao.Verify(x => x.Add(It.IsAny<User>()), Times.Once());
        //    Assert.AreEqual(newUser, created);
        //}

        //[Test]
        //public void GetAllUsers_ShouldReturnUserList()
        //{
        //    List<User> users = new List<User>
        //    {
        //        new User() {UserId = "Unit"},
        //        new User() {UserId = "Test"},
        //        new User() {UserId = "Dao"}
        //    };

        //    var mockContext = new Mock<UserDataContext>();
        //    var mockDao = new Mock<UserDAO>(mockContext.Object);
        //    mockDao.Setup(x => x.All()).Returns(users);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.UserDAO).Returns(mockDao.Object);

        //    var getUsers = svc.Object.GetAllUsers();
        //    mockDao.Verify(x => x.All(), Times.Once());
        //    Assert.AreEqual(users.Count, getUsers.Count);
        //}

        //[Test]
        //public void GetDevice_ShouldCallGetByName()
        //{
        //    Device d = new Device() { DeviceId = "Test" };

        //    var mockContext = new Mock<UserDataContext>();
        //    var mockDao = new Mock<DeviceDAO>(mockContext.Object);
        //    mockDao.Setup(x => x.GetByName(It.IsAny<string>())).Returns(d);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.DeviceDAO).Returns(mockDao.Object);

        //    var device = svc.Object.GetDevice("Test");
        //    mockDao.Verify(x => x.GetByName(It.IsAny<string>()), Times.Once());
        //    Assert.AreEqual(d, device);
        //}

        //[Test]
        //public void GetDevices_ShouldReturnDeviceList()
        //{
        //    List<Device> devices = new List<Device>
        //    {
        //        new Device() {DeviceId = "Unit"},
        //        new Device() {DeviceId = "Test"},
        //        new Device() {DeviceId = "Dao"}
        //    };

        //    var mockContext = new Mock<UserDataContext>();
        //    var mockDao = new Mock<DeviceDAO>(mockContext.Object);
        //    mockDao.Setup(x => x.All()).Returns(devices);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.DeviceDAO).Returns(mockDao.Object);

        //    var allDevices = svc.Object.GetDevices();
        //    mockDao.Verify(x => x.All(), Times.Once());
        //    Assert.AreEqual(devices.Count, allDevices.Count);
        //}

        //[Test]
        //public void GetUser_ShouldCallDaoGetByUserId()
        //{
        //    User user = new User() { UserId = "Test" };
        //    var mockContext = new Mock<UserDataContext>();
        //    var mockDao = new Mock<UserDAO>(mockContext.Object);
        //    mockDao.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns(user);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.UserDAO).Returns(mockDao.Object);

        //    var returnedUser = svc.Object.GetUser("Test");
        //    mockDao.Verify(x => x.GetByUserId(It.IsAny<string>()), Times.Once());
        //    Assert.AreEqual(user, returnedUser);
        //}

        //[Test]
        //public void LinkDeviceToUser_ShouldCallDeviceDaoUpdate()
        //{
        //    var user = new User() { UserId = "Test" };
        //    var device = new Device() { DeviceId = "DeviceTest" };

        //    var mockContext = new Mock<UserDataContext>();
        //    var userDao = new Mock<UserDAO>(mockContext.Object);
        //    var deviceDao = new Mock<DeviceDAO>(mockContext.Object);

        //    userDao.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns(user);
        //    deviceDao.Setup(x => x.GetByName(It.IsAny<string>())).Returns(device);
        //    deviceDao.Setup(x => x.Update(It.IsAny<Device>())).Returns(device);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.UserDAO).Returns(userDao.Object);
        //    svc.Setup(x => x.DeviceDAO).Returns(deviceDao.Object);

        //    svc.Object.LinkDeviceToUser("DeviceTest", "Test");
        //    deviceDao.Verify(x => x.Update(It.IsAny<Device>()), Times.Once());
        //}

        //[Test]
        //public void LinkDeviceToUser_ShouldThrowExceptionWhenUserOrDeviceIsNull()
        //{
        //    var device = new Device() { DeviceId = "DeviceTest" };
        //    User user = null;

        //    var mockContext = new Mock<UserDataContext>();
        //    var userDao = new Mock<UserDAO>(mockContext.Object);
        //    var deviceDao = new Mock<DeviceDAO>(mockContext.Object);

        //    userDao.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns(user);
        //    deviceDao.Setup(x => x.GetByName(It.IsAny<string>())).Returns(device);
        //    deviceDao.Setup(x => x.Update(It.IsAny<Device>())).Returns(device);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.UserDAO).Returns(userDao.Object);
        //    svc.Setup(x => x.DeviceDAO).Returns(deviceDao.Object);

        //    Assert.That(() => svc.Object.LinkDeviceToUser("DeviceTest", "Test"), Throws.Exception.With.Property("Detail").EqualTo("User or Device not found."));
        //    deviceDao.Verify(x => x.Update(It.IsAny<Device>()), Times.Never());
        //}

        //[Test]
        //public void RegisterDevice_ShouldAddDeviceTypeIfNotExistAndAddNewDevice()
        //{
        //    var device = new Device() { DeviceId = "Test" };
        //    var deviceType = new DeviceType() { Type = "Type", Unit = "°C" };

        //    var mockContext = new Mock<UserDataContext>();
        //    var deviceDao = new Mock<DeviceDAO>(mockContext.Object);
        //    var deviceTypeDao = new Mock<DeviceTypeDAO>(mockContext.Object);

        //    deviceTypeDao.Setup(x => x.Add(It.IsAny<DeviceType>())).Returns(deviceType);
        //    deviceTypeDao.Setup(x => x.GetByTypeName(It.IsAny<string>())).Returns((DeviceType)null);

        //    deviceDao.Setup(x => x.Add(It.IsAny<Device>())).Returns(device);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.DeviceDAO).Returns(deviceDao.Object);
        //    svc.Setup(x => x.DeviceTypeDAO).Returns(deviceTypeDao.Object);

        //    svc.Object.RegisterDevice("", "", "");

        //    deviceTypeDao.Verify(x => x.Add(It.IsAny<DeviceType>()), Times.Once());
        //    deviceDao.Verify(x => x.Add(It.IsAny<Device>()), Times.Once());
        //}

        //[Test]
        //public void RegisterDevice_ShouldAndAddNewDevice()
        //{
        //    var device = new Device() { DeviceId = "Test" };
        //    var deviceType = new DeviceType() { Type = "Type", Unit = "°C" };

        //    var mockContext = new Mock<UserDataContext>();
        //    var deviceDao = new Mock<DeviceDAO>(mockContext.Object);
        //    var deviceTypeDao = new Mock<DeviceTypeDAO>(mockContext.Object);

        //    deviceTypeDao.Setup(x => x.GetByTypeName(It.IsAny<string>())).Returns(deviceType);

        //    deviceDao.Setup(x => x.Add(It.IsAny<Device>())).Returns(device);

        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.DeviceDAO).Returns(deviceDao.Object);
        //    svc.Setup(x => x.DeviceTypeDAO).Returns(deviceTypeDao.Object);

        //    svc.Object.RegisterDevice("", "", "");

        //    deviceTypeDao.Verify(x => x.Add(It.IsAny<DeviceType>()), Times.Never());
        //    deviceDao.Verify(x => x.Add(It.IsAny<Device>()), Times.Once());
        //}

        //[Test]
        //public void RemoveUser_ShouldCallRemoveByUserIdOnDao()
        //{
        //    var mockContext = new Mock<UserDataContext>();
        //    var userDao = new Mock<UserDAO>(mockContext.Object);
        //    userDao.Setup(x => x.RemoveByUserId(It.IsAny<string>())).Returns(true);
        //    var svc = new Mock<UserDataService>(mockContext.Object);
        //    svc.Setup(x => x.UserDAO).Returns(userDao.Object);

        //    svc.Object.RemoveUser("");

        //    userDao.Verify(x => x.RemoveByUserId(It.IsAny<string>()), Times.Once());
        //}
    }
}
