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
    public class AdminDaoTests
    {
        Mock<DbSet<Admin>> SetupDbSet(IQueryable<Admin> data)
        {
            var mockSet = new Mock<DbSet<Admin>>();

            mockSet.As<IQueryable<Admin>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Admin>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Admin>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Admin>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        [Test]
        public void GivenNewEntityShouldAddInDb()
        {
            var data = new List<Admin>().AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);
            mockContext.Setup(c => c.Admin).Returns(mockSet.Object);

            var dao = new AdminDAO(mockContext.Object);
            dao.Add(new Admin() { AdminId = "Admin", Password = "password" });

            mockSet.Verify(m => m.Add(It.IsAny<Admin>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void GivenEntityWithoutPasswordShouldThrowException()
        {
            var data = new List<Admin>().AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);
            mockContext.Setup(c => c.Admin).Returns(mockSet.Object);

            var dao = new AdminDAO(mockContext.Object);

            var ex = Assert.Throws<Exception>(() => dao.Add(new Admin() { AdminId = "Admin" }));
            Assert.AreEqual("Identifier and Password cannot be empty", ex.Message);
        }

        [Test]
        public void GivenEntityWithoutIdentifierShouldThrowException()
        {
            var data = new List<Admin>().AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);
            mockContext.Setup(c => c.Admin).Returns(mockSet.Object);

            var dao = new AdminDAO(mockContext.Object);

            var ex = Assert.Throws<Exception>(() => dao.Add(new Admin() { AdminId = "" }));
            Assert.AreEqual("Identifier and Password cannot be empty", ex.Message);
        }

        [Test]
        public void GivenExistingEntityShouldThrowException()
        {
            var data = new List<Admin> { new Admin() { Id = 0, AdminId = "Admin", Password = "password" } }.AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);
            mockContext.Setup(c => c.Admin).Returns(mockSet.Object);

            var dao = new AdminDAO(mockContext.Object);

            var ex = Assert.Throws<Exception>(() => dao.Add(new Admin() { AdminId = "Admin", Password = "password" }));
            Assert.AreEqual("Admin Identifier already exists.", ex.Message);
        }

        [Test]
        public void ShouldReturnAllEntities()
        {
            var data = new List<Admin>
            {
                new Admin() { Id = 0, AdminId = "Admin1", Password = "pass"},
                new Admin() { Id = 1, AdminId = "Admin2", Password = "pass"},
                new Admin() { Id = 2, AdminId = "Admin3", Password = "pass"}
            }.AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);
            mockContext.Setup(c => c.Admin).Returns(mockSet.Object);

            var dao = new AdminDAO(mockContext.Object);
            var results = dao.All();

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("Admin1", results[0].AdminId);
            Assert.AreEqual("Admin2", results[1].AdminId);
            Assert.AreEqual("Admin3", results[2].AdminId);
        }

        [Test]
        public void ShouldReturnAllEntitiesAsync()
        {

        }

        [Test]
        public void GivenIdShouldReturnEntity()
        {
            Admin admin = new Admin() { Id = 0, AdminId = "Admin1", Password = "pass" };

            var mockContext = new Mock<UserDataContext>();
            mockContext.Setup(c => c.Admin.Find(0)).Returns(admin);

            var dao = new AdminDAO(mockContext.Object);
            var result = dao.Get(0);

            Assert.AreEqual(admin, result);
        }

        [Test]
        public void GivenIdShouldReturnEntityAsync()
        {

        }

        [Test]
        public void GivenIdShouldRemoveFromDb()
        {
            Admin admin = new Admin() { Id = 0, AdminId = "Admin1", Password = "pass" };

            var mockContext = new Mock<UserDataContext>();
            mockContext.Setup(c => c.Admin.Find(0)).Returns(admin);

            var dao = new AdminDAO(mockContext.Object);
            dao.Remove(0);

            mockContext.Verify(c => c.Admin.Remove(It.IsAny<Admin>()), Times.Once());
        }

        [Test]
        public void GivenUpdatedPasswordShouldUpdateEntityInDb()
        {
            Admin oldAdmin = new Admin() { Id = 0, AdminId = "Admin", Password = "password" };
            Admin newAdmin = new Admin() { Id = 0, AdminId = "Admin", Password = "newpassword" };

            var mockContext = new Mock<UserDataContext>();

            mockContext.Setup(c => c.Admin.Find(0)).Returns(oldAdmin);

            var dao = new AdminDAO(mockContext.Object);
            var result = dao.Update(newAdmin);

            Assert.AreEqual("newpassword", result.Password);
        }

        [Test]
        public void GivenUpdatedAdminIdFieldOfEntityShouldNotUpdateInDbAndThrowException()
        {
            Admin oldAdmin = new Admin() { Id = 0, AdminId = "Admin", Password = "password" };
            Admin newAdmin = new Admin() { Id = 0, AdminId = "AdminNew", Password = "password" };

            var mockContext = new Mock<UserDataContext>();

            mockContext.Setup(c => c.Admin.Find(0)).Returns(oldAdmin);

            var dao = new AdminDAO(mockContext.Object);
            var ex = Assert.Throws<Exception>(() => dao.Update(newAdmin));
            
            Assert.AreEqual("You cannot update the Admin Identifier.", ex.Message);
        }
    }
}
