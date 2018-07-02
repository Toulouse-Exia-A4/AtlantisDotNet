using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.UserData.DAL.Tests
{
    [TestFixture]
    public class DeviceDaoTests
    {

        Mock<DbSet<Device>> SetupDbSet(IQueryable<Device> data)
        {
            var mockSet = new Mock<DbSet<Device>>();

            mockSet.As<IQueryable<Device>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Device>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Device>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Device>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        [Test]
        public void GivenValidObjectShouldAddObject()
        {
            var data = new List<Device>().AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);

            mockContext.Setup(c => c.Device).Returns(mockSet.Object);
            mockContext.Setup(c => c.DeviceType.Find(0)).Returns(new DeviceType() { Id = 0, Type = "Temperature" });

            var dao = new DeviceDAO(mockContext.Object);
            var newUserType = dao.Add(new Device() { DeviceId = "AAAAAA", DeviceTypeId = 0 });

            mockSet.Verify(m => m.Add(It.IsAny<Device>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void GivenValidObjectWithUserIdShouldAddObject()
        {
            var data = new List<Device>().AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);

            mockContext.Setup(c => c.Device).Returns(mockSet.Object);
            mockContext.Setup(c => c.DeviceType.Find(0)).Returns(new DeviceType() { Id = 0, Type = "Temperature" });
            mockContext.Setup(c => c.User.Find(0)).Returns(new User() { Id = 0, UserId = "ABCDEF" });
            mockSet.Setup(c => c.Add(It.IsAny<Device>())).Returns(new Device() { DeviceId = "AAAAAA", DeviceTypeId = 0, UserId = 0 });

            var dao = new DeviceDAO(mockContext.Object);
            var newUserType = dao.Add(new Device() { DeviceId = "AAAAAA", DeviceTypeId = 0, UserId = 0 });

            mockSet.Verify(m => m.Add(It.IsAny<Device>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void GivenObjectWithInvalidTypeIdShouldThrowException()
        {
            var data = new List<Device>().AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);

            mockContext.Setup(c => c.Device).Returns(mockSet.Object);
            mockContext.Setup(c => c.DeviceType.Find(0)).Returns((DeviceType)null);

            var dao = new DeviceDAO(mockContext.Object);

            var ex = Assert.Throws<Exception>(() => dao.Add(new Device() { DeviceId = "AAAAAA", DeviceTypeId = 0 }));
            Assert.AreEqual("Device.TypeId doesn't exist.", ex.Message);
        }

        [Test]
        public void ShouldReturnAllDevices()
        {
            var data = new List<Device>
            {
                new Device() {DeviceId = "A"},
                new Device() {DeviceId = "B"}
            }.AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            mockContext.Setup(c => c.Device).Returns(SetupDbSet(data).Object);

            var dao = new DeviceDAO(mockContext.Object);
            var results = dao.All();
            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void ShouldReturnAllDevicesAsync()
        {
            // TODO
        }

        [Test]
        public void GivenIdShouldReturnObject()
        {
            Device d1 = new Device() { Id = 0, DeviceId = "A" };
            Device d2 = new Device() { Id = 1, DeviceId = "B" };

            var mockContext = new Mock<UserDataContext>();

            mockContext.Setup(c => c.Device.Find(0)).Returns(d1);

            var dao = new DeviceDAO(mockContext.Object);
            var result = dao.Get(0);
            Assert.AreEqual(d1, result);
        }

        [Test]
        public void GivenIdShouldReturnObjectAsync()
        {
            // TODO
        }

        [Test]
        public void GivenIdShouldRemoveFromDb()
        {
            Device d = new Device() { Id = 0, DeviceId = "A" };

            var mockContext = new Mock<UserDataContext>();
            mockContext.Setup(c => c.Device.Find(0)).Returns(d);
            
            var dao = new DeviceDAO(mockContext.Object);
            dao.Remove(0);

            mockContext.Verify(m => m.Device.Remove(It.IsAny<Device>()), Times.Once());
        }

        [Test]
        public void GivenValidObjectShouldUpdateInDb()
        {
            Device d = new Device() { Id = 0, DeviceId = "A" };
            Device dUpd = new Device() { Id = 0, DeviceId = "A", UserId = 0 };

            var mockContext = new Mock<UserDataContext>();
            mockContext.Setup(c => c.Device.Find(0)).Returns(d);

            var dao = new DeviceDAO(mockContext.Object);
            var updatedObj = dao.Update(dUpd);

            Assert.AreEqual("A", updatedObj.DeviceId);
            Assert.AreEqual(0, updatedObj.UserId);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void GivenDeviceTypeShouldReturnAllDeviceOfType()
        {
            var data = new List<Device>
            {
                new Device() {Id = 0, DeviceId = "A", DeviceTypeId = 0},
                new Device() {Id = 1, DeviceId = "B", DeviceTypeId = 1},
                new Device() {Id = 2, DeviceId = "C", DeviceTypeId = 0}
            }.AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);

            mockContext.Setup(c => c.Device).Returns(mockSet.Object);

            var dao = new DeviceDAO(mockContext.Object);
            var results = dao.GetAllDevicesOfType(new DeviceType() { Id = 0 });

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("A", results[0].DeviceId);
            Assert.AreEqual("C", results[1].DeviceId);
        }

        [Test]
        public void GivenDeviceNameShouldReturnDevice()
        {
            var data = new List<Device>
            {
                new Device() {Id = 0, DeviceId = "A", UserId = 0},
                new Device() {Id = 1, DeviceId = "B", UserId = 1},
                new Device() {Id = 2, DeviceId = "C", UserId = 0}
            }.AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);

            mockContext.Setup(c => c.Device).Returns(mockSet.Object);

            var dao = new DeviceDAO(mockContext.Object);
            var result = dao.GetByName("A");

            Assert.AreEqual("A", result.DeviceId);
        }
    }
}
