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
    public class DeviceTypeDaoTests
    {
        Mock<DbSet<DeviceType>> SetupDbSet(IQueryable<DeviceType> data)
        {
            var mockSet = new Mock<DbSet<DeviceType>>();

            mockSet.As<IQueryable<DeviceType>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<DeviceType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<DeviceType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<DeviceType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        [Test]
        public void GivenNewEntityShouldAddInDb()
        {
            var data = new List<DeviceType>().AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);

            mockContext.Setup(c => c.DeviceType).Returns(mockSet.Object);

            var dao = new DeviceTypeDAO(mockContext.Object);
            dao.Add(new DeviceType() { Id = 0, Type = "Pression" });

            mockSet.Verify(m => m.Add(It.IsAny<DeviceType>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void GivenExistingDeviceTypeShouldThrowException()
        {
            var data = new List<DeviceType>
            {
                new DeviceType() {Id = 12, Type = "Pression"}
            }.AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);

            mockContext.Setup(c => c.DeviceType).Returns(mockSet.Object);

            var dao = new DeviceTypeDAO(mockContext.Object);

            var ex = Assert.Throws<Exception>(() => dao.Add(new DeviceType() { Id = 0, Type = "Pression" }));
            Assert.AreEqual("DeviceType.Type already exists.", ex.Message);
        }

        [Test]
        public void ShouldReturnAllDeviceType()
        {
            var data = new List<DeviceType>
            {
                new DeviceType() {Id = 0, Type = "Pression"},
                new DeviceType() {Id = 1, Type = "Tpt"}
            }.AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);

            mockContext.Setup(c => c.DeviceType).Returns(mockSet.Object);

            var dao = new DeviceTypeDAO(mockContext.Object);
            var results = dao.All();

            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void ShouldReturnAllDeviceTypeAsync()
        {

        }

        [Test]
        public void GivenIdShoudReturnEntity()
        {
            DeviceType dt = new DeviceType() { Id = 0, Type = "Pression" };

            var mockContext = new Mock<UserDataContext>();

            mockContext.Setup(c => c.DeviceType.Find(0)).Returns(dt);

            var dao = new DeviceTypeDAO(mockContext.Object);
            var result = dao.Get(0);

            Assert.AreEqual(dt, result);
        }

        [Test]
        public void GivenIdShoudReturnEntityAsync()
        {

        }

        [Test]
        public void GivenIdShouldRemoveFromDb()
        {
            DeviceType dt = new DeviceType() { Id = 0, Type = "Test" };

            var mockContext = new Mock<UserDataContext>();

            mockContext.Setup(c => c.DeviceType.Find(0)).Returns(dt);

            var dao = new DeviceTypeDAO(mockContext.Object);
            dao.Remove(0);

            mockContext.Verify(m => m.DeviceType.Remove(It.IsAny<DeviceType>()), Times.Once());
        }

        [Test]
        public void GivenModifiedEntityShouldUpdateInDbAndReturnUpdatedEntity()
        {
            DeviceType dt = new DeviceType() { Id = 0, Type = "OriginalValue", Unit = "Test" };

            var mockContext = new Mock<UserDataContext>();
            mockContext.Setup(c => c.DeviceType.Find(0)).Returns(dt);

            var dao = new DeviceTypeDAO(mockContext.Object);
            var result = dao.Update(new DeviceType() { Id = 0, Type = "NewValue", Unit = "NewTest" });

            Assert.AreEqual("NewValue", result.Type);
            Assert.AreEqual("NewTest", result.Unit);
            Assert.AreEqual(0, result.Id);
        }


    }
}
