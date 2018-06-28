using Atlantis.UserData.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Atlantis.UserData.Service
{
    public class UserDataService : IUserDataService
    {
        public Device AddDevice(Device device)
        {
            using (var ctx = new UserDataContext())
            {
                var dao = new DeviceDAO(ctx);
                return dao.Add(device);
            }
        }

        public User Create(string userId)
        {
            using (var ctx = new UserDataContext())
            {
                var dao = new UserDAO(ctx);
                return dao.Add(new User() { UserId = userId });
            }
        }

        public List<User> GetAllUsers()
        {
            using (var ctx = new UserDataContext())
            {
                var dao = new UserDAO(ctx);
                return dao.All();
            }
        }

        public Device GetDevice(string deviceId)
        {
            using (var ctx = new UserDataContext())
            {
                var dao = new DeviceDAO(ctx);
                return dao.GetByName(deviceId);
            }
        }

        public List<Device> GetDevices()
        {
            using (var ctx = new UserDataContext())
            {
                var dao = new DeviceDAO(ctx);
                return dao.All();
            }
        }

        public User GetUser(string userId)
        {
            using (var ctx = new UserDataContext())
            {
                var dao = new UserDAO(ctx);
                return dao.GetByUserId(userId);
            }
        }

        public List<Device> GetUserDevices(string userId)
        {
            using (var ctx = new UserDataContext())
            {
                var dao = new UserDAO(ctx);
                return dao.GetUserDevices(userId);
            }
        }

        public void Remove(string userId)
        {
            using (var ctx = new UserDataContext())
            {
                var dao = new UserDAO(ctx);
                dao.RemoveByUserId(userId);
            }
        }
    }
}
