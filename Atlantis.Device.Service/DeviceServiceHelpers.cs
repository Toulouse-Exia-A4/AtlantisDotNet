using Atlantis.UserData.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.Device.Service
{
    public class DeviceServiceHelpers
    {
        public void AddDevice(DeviceMetricModel deviceMetricModel, DeviceTypeDAO deviceTypeDAO, DeviceDAO deviceDAO)
        {
            try
            {
                if(!ValidateDeviceMetricModel(deviceMetricModel))
                {
                    throw new WebFaultException<string>("deviceId or value is missing from request body.", HttpStatusCode.BadRequest);
                }

                var type = deviceTypeDAO.GetByTypeName(deviceMetricModel.DeviceType);

                if (type == null)
                {
                    DeviceType dt = new DeviceType()
                    {
                        Type = deviceMetricModel.DeviceType,
                        Unit = deviceMetricModel.DeviceUnit
                    };

                    type = deviceTypeDAO.Add(dt);
                }

                UserData.DAL.Device d = new UserData.DAL.Device()
                {
                    DeviceId = deviceMetricModel.DeviceId,
                    DeviceTypeId = type.Id
                };

                deviceDAO.Add(d);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void SendMetricToQueue(DeviceMetricModel model)
        {
            try
            {
                // TODO
                Console.WriteLine(model.DeviceId + " metrics : " + model.MetricValue);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>("Error trying to send metric to JMS Queue : " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public bool ValidateDeviceMetricModel(DeviceMetricModel model)
        {
            return model.DeviceId != null && model.DeviceId.Length > 0 && model.DeviceType != null && model.DeviceType.Length > 0 &&
                model.MetricValue != null && model.MetricValue.Length > 0;
        }
    }
}
