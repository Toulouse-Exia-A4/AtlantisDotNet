using Atlantis.UserData.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Atlantis.Device.Service
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "DeviceService" à la fois dans le code et le fichier de configuration.
    public class DeviceService : IDeviceService
    {
        private UserDataContext _context;

        public virtual DeviceDAO DeviceDAO
        {
            get { return new DeviceDAO(_context); }
        }

        public virtual DeviceTypeDAO DeviceTypeDAO
        {
            get { return new DeviceTypeDAO(_context); }
        }

        public virtual DeviceServiceHelpers DeviceServiceHelper
        {
            get { return new DeviceServiceHelpers(); }
        }

        public DeviceService()
        {
            _context = new UserDataContext();
        }

        public DeviceService(UserDataContext context)
        {
            _context = context;
        }

        public void AddRawMetric(DeviceMetricModel deviceMetricModel)
        {
            try
            {
                if (!DeviceServiceHelper.ValidateDeviceMetricModel(deviceMetricModel))
                    throw new WebFaultException<string>("deviceId or value is missing from request body.", HttpStatusCode.BadRequest);

                var device = DeviceDAO.GetByName(deviceMetricModel.DeviceId);

                if (device == null)
                {
                    DeviceServiceHelper.AddDevice(deviceMetricModel, DeviceTypeDAO, DeviceDAO);
                }

                DeviceServiceHelper.SendMetricToQueue(deviceMetricModel);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
