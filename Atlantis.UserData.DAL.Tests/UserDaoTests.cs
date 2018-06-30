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
    public class UserDaoTests
    {
        Mock<DbSet<User>> SetupDbSet(IQueryable<User> data)
        {
            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        // Add
        [Test]
        public void GivenNewUserShouldAddInDb()
        {
            var data = new List<User>().AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);
            mockContext.Setup(c => c.User).Returns(mockSet.Object);

            var dao = new UserDAO(mockContext.Object);
            dao.Add(new User() { Id = 0, UserId = "XXXX" });

            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        // Add
        [Test]
        public void GivenExistingUserForCreationShouldThrowException()
        {
            var data = new List<User> { new User() { Id = 0, UserId = "XXXX" } }.AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);
            mockContext.Setup(c => c.User).Returns(mockSet.Object);

            var dao = new UserDAO(mockContext.Object);

            var ex = Assert.Throws<Exception>(() => dao.Add(new User() { UserId = "XXXX" }));
            Assert.AreEqual("User already registered.", ex.Message);
        }

        // All()
        [Test]
        public void ShouldReturnAllUsers()
        {
            var data = new List<User>
            {
                new User() { Id = 0, UserId = "XXXX" },
                new User() { Id = 1, UserId = "XX" }
            }.AsQueryable();

            var mockContext = new Mock<UserDataContext>();
            var mockSet = SetupDbSet(data);

            mockContext.Setup(c => c.User).Returns(mockSet.Object);

            var dao = new UserDAO(mockContext.Object);
            var results = dao.All();

            Assert.AreEqual(2, results.Count);
        }

        // AllAsync()
        [Test]
        public void ShouldReturnAllUsersAsync()
        {
            // TODO
        }

        // Get
        [Test]
        public void GivenIdShouldReturnUser()
        {
            User user = new User() { Id = 0, UserId = "Test" };

            var mockContext = new Mock<UserDataContext>();

            mockContext.Setup(c => c.User.Find(0)).Returns(user);

            var dao = new UserDAO(mockContext.Object);
            var result = dao.Get(0);

            Assert.AreEqual(user, result);
        }

        // GetAsync
        [Test]
        public void GivenIdShouldReturnUserAsync()
        {
            // TODO
        }

        // Remove
        [Test]
        public void GivenIdShouldRemoveFromDb()
        {
            User user = new User() { Id = 0, UserId = "Test" };

            var mockContext = new Mock<UserDataContext>();

            mockContext.Setup(c => c.User.Find(0)).Returns(user);

            var dao = new UserDAO(mockContext.Object);
            dao.Remove(0);

            mockContext.Verify(m => m.User.Remove(It.IsAny<User>()), Times.Once());
        }

        // RemoveByUserId
        [Test]
        public void GivenUserIdShouldRemoveFromDb()
        {
            var data = new List<User>
            {
                new User() { Id = 0, UserId = "Test" },
                new User() { Id = 1, UserId = "toto" }
            }.AsQueryable();

            var mockSet = SetupDbSet(data);

            var mockContext = new Mock<UserDataContext>();
            mockContext.Setup(c => c.User).Returns(mockSet.Object);

            var dao = new UserDAO(mockContext.Object);
            bool res = dao.RemoveByUserId("Test");

            Assert.AreEqual(true, res);
        }

        // RemoveByUserId
        [Test]
        public void GivenWrongUserIdShouldNotRemoveFromDb()
        {
            var data = new List<User>
            {
                new User() { Id = 0, UserId = "Test" },
                new User() { Id = 1, UserId = "toto" }
            }.AsQueryable();

            var mockSet = SetupDbSet(data);

            var mockContext = new Mock<UserDataContext>();
            mockContext.Setup(c => c.User).Returns(mockSet.Object);

            var dao = new UserDAO(mockContext.Object);
            bool res = dao.RemoveByUserId("UnitTest");

            Assert.AreEqual(false, res);
        }

        // Update
        [Test]
        public void GivenNewUserToUpdateShouldThrowException()
        {
            User newUser = new User() { Id = 0, UserId = "UpdateUser" };

            var mockContext = new Mock<UserDataContext>();

            var dao = new UserDAO(mockContext.Object);
            var ex = Assert.Throws<Exception>(() => dao.Update(newUser));
            Assert.AreEqual("User cannot be modified.", ex.Message);
        }

        [Test]
        public void GivenUserIdShouldReturnUser()
        {
            var data = new List<User>
            {
                new User() { Id = 0, UserId = "Test" },
                new User() { Id = 1, UserId = "toto" }
            }.AsQueryable();

            var mockSet = SetupDbSet(data);

            var mockContext = new Mock<UserDataContext>();
            mockContext.Setup(c => c.User).Returns(mockSet.Object);

            var dao = new UserDAO(mockContext.Object);
            var result = dao.GetByUserId("Test");

            Assert.AreEqual("Test", result.UserId);
        }

    }
}
