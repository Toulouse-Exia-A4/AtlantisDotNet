using System;
using System.Collections.Generic;
using System.Data.Entity;
using Atlantis.UserData.DAL;
using Moq;
using NUnit.Framework;

namespace Atlantis.UserData.Service.Tests
{
    [TestFixture]
    public class UserDataServiceTests
    {
        [Test]
        public void GetUser_GivenExistingUserIdShouldReturnUser()
        {

        }

        [Test]
        public void GetUser_GivenNonExistingUserIdShouldReturnNull()
        {

        }

        [Test]
        public void GetUser_GivenEmptyUserIdShouldThrowException()
        {

        }

        [Test]
        public void GetUser_GivenNullUserIdShouldThrowException()
        {

        }

        [Test]
        public void AddUser_GivenUserIdFirstnameLastnameParametersShouldAddUser()
        {

        }

        [Test]
        public void AddUser_GivenMissingParameterShouldThrowException()
        {

        }

        [Test]
        public void AddUser_GivenExistingUserIdShouldThrowException()
        {

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
