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
                                DeviceId = device.Id,
                                Name = device.Name,
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
    }
}