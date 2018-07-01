using Atlantis.UserData.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Atlantis.BackOffice.Service
{
    public class BackOfficeService : IBackOfficeService
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

        public BackOfficeService()
        {
            _context = new UserDataContext();
        }

        public BackOfficeService(UserDataContext context)
        {
            _context = context;
        }

        public List<DeviceModel> GetDevices()
        {
            try
            {
                var devices = DeviceDAO.All();
                var results = new List<DeviceModel>();

                foreach (var device in devices)
                {
                    results.Add(new DeviceModel()
                    {
                        DeviceId = device.DeviceId,
                        DeviceType = device.DeviceType != null ? device.DeviceType.Type : null
                    });
                }

                return results;
            }
            catch(Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public List<UserModel> GetUsers()
        {
            try
            {
                var users = UserDAO.All();
                var results = new List<UserModel>();

                foreach (var user in users)
                {
                    UserModel userModel = new UserModel();
                    userModel.UserId = user.UserId;
                    userModel.Firstname = user.Firstname;
                    userModel.Lastname = user.Lastname;
                    userModel.Devices = new List<DeviceModel>();

                    foreach(var d in user.Device)
                    {
                        userModel.Devices.Add(new DeviceModel()
                        {
                            DeviceId = d.DeviceId,
                            DeviceType = d.DeviceType != null ? d.DeviceType.Type : null
                        });
                    }

                    results.Add(userModel);
                }

                return results;
            }
            catch(Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public bool LinkDeviceToUser(string userId, string deviceId)
        {
            try
            {
                if (userId == null || userId.Length == 0 || deviceId == null || deviceId.Length == 0)
                    throw new WebFaultException<string>("LinkDeviceToUser missing parameter.", HttpStatusCode.BadRequest);

                UserDAO
            }
            catch(WebFaultException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
