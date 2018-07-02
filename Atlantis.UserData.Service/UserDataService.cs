using Atlantis.UserData.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Atlantis.UserData.Service
{
    public class UserDataService : IUserDataService
    {
        private UserDataContext _context;

        public virtual UserDAO UserDAO
        {
            get { return new UserDAO(_context); }
        }

        public virtual DeviceDAO DeviceDAO
        {
            get { return new DeviceDAO(_context); }
        }

        public virtual AdminDAO AdminDAO
        {
            get { return new AdminDAO(_context); }
        }

        public virtual DeviceTypeDAO DeviceTypeDAO
        {
            get { return new DeviceTypeDAO(_context); }
        }

        public UserDataService()
        {
            _context = new UserDataContext();
        }

        public UserDataService(UserDataContext ctx)
        {
            _context = ctx;
        }

        // Methods
        public UserModel GetUser(string userId)
        {
            try
            {
                if (userId == null || userId.Length == 0)
                    throw new Exception("userId parameter is null or empty.");

                var user = UserDAO.GetByUserId(userId);
                if (user != null)
                {
                    UserModel userModel = new UserModel() { UserId = user.UserId, Firstname = user.Firstname, Lastname = user.Lastname };

                    var devices = UserDAO.GetUserDevices(user);

                    if(devices != null)
                    {
                        List<DeviceModel> deviceModels = new List<DeviceModel>();

                        foreach (var device in devices)
                        {
                            deviceModels.Add(new DeviceModel()
                            {
                                DeviceId = device.DeviceId,
                                DeviceType = new DeviceTypeModel() { Type = device.DeviceType.Type, Unit = device.DeviceType.Unit }
                            });
                        }

                        userModel.Devices = deviceModels;
                    }
                    
                    return userModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        public UserModel AddUser(string userId, string firstname, string lastname)
        {
            try
            {
                if (
                    userId == null || userId.Length == 0 ||
                    firstname == null || firstname.Length == 0 ||
                    lastname == null || lastname.Length == 0
                   )
                    throw new Exception("Missing parameter to add new user.");

                var newUser = UserDAO.Add(new User() { UserId = userId, Firstname = firstname, Lastname = lastname });

                if (newUser == null)
                    throw new Exception("User already registered.");

                return new UserModel()
                {
                    UserId = newUser.UserId,
                    Firstname = newUser.Firstname,
                    Lastname = newUser.Lastname
                };
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        #region oldcode
        //public Device AddDevice(Device device)
        //{
        //    try
        //    {
        //        return DeviceDAO.Add(device);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        //public User CreateUser(string userId)
        //{
        //    try
        //    {
        //        return UserDAO.Add(new User() { UserId = userId });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        //public List<User> GetAllUsers()
        //{
        //    try
        //    {
        //        return UserDAO.All();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        //public Device GetDevice(string deviceId)
        //{
        //    try
        //    {
        //        return DeviceDAO.GetByName(deviceId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        //public Models.Device[] GetDevices()
        //{
        //    try
        //    {
        //        var results = DeviceDAO.All();

        //        List<Models.Device> devices = new List<Models.Device>();
        //        foreach(var r in results)
        //        {
        //            devices.Add(new Models.Device()
        //            {
        //                DeviceId = r.DeviceId,
        //                User = r.UserId.HasValue ? new Models.User() { UserId = r.UserId.Value.ToString() }
        //            });
        //        }

        //        return devices.ToArray();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        //public User GetUser(string userId)
        //{
        //    try
        //    {
        //        return UserDAO.GetByUserId(userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        //public List<Device> GetUserDevices(string userId)
        //{
        //    try
        //    {
        //        return DeviceDAO.GetAllDevicesOfUser(new User() { UserId = userId });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        //public void LinkDeviceToUser(string deviceId, string userId)
        //{
        //    try
        //    {
        //        var user = UserDAO.GetByUserId(userId);
        //        var device = DeviceDAO.GetByName(deviceId);

        //        if (user != null && device != null)
        //        {
        //            device.UserId = user.Id;
        //            DeviceDAO.Update(device);
        //        }
        //        else
        //        {
        //            throw new Exception("User or Device not found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.BadRequest);
        //    }
        //}

        //public void RegisterDevice(string deviceId, string deviceType, string deviceUnit = "")
        //{
        //    try
        //    {
        //        var dt = DeviceTypeDAO.GetByTypeName(deviceType);

        //        if (dt == null)
        //        {
        //            dt = new DeviceType() { Type = deviceType, Unit = deviceUnit };
        //            dt = DeviceTypeDAO.Add(dt);
        //        }

        //        Device device = new Device() { DeviceTypeId = dt.Id, DeviceId = deviceId };
        //        DeviceDAO.Add(device);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        //public void RemoveUser(string userId)
        //{
        //    try
        //    {
        //        UserDAO.RemoveByUserId(userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}
        #endregion
    }
}
