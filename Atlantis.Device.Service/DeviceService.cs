using Atlantis.UserData.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebLogic.Messaging;

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

        public DeviceService()
        {
            _context = new UserDataContext();
        }

        public DeviceService(UserDataContext context)
        {
            _context = context;
        }

        public void AddRawMetric(int id, string date, string value)
        {
            IDictionary<string, Object> paramMap = new Dictionary<string, Object>();
            paramMap[Constants.Context.PROVIDER_URL] = ConfigurationManager.AppSettings["jmsProviderUrl"];

            IContext jmsContext = ContextFactory.CreateContext(paramMap);
            IConnectionFactory cf = jmsContext.LookupConnectionFactory(ConfigurationManager.AppSettings["jmsConnectionFactory"]);
            ITopic topic = (ITopic)jmsContext.LookupDestination(ConfigurationManager.AppSettings["jmsTopic"]);

            IConnection connection;

            try
            {
                if (date == null || date.Length == 0 || value == null || value.Length == 0)
                    throw new Exception("Missing parameters.");

                var device = DeviceDAO.Get(id);

                if (device == null)
                    throw new Exception("Device doesn't exist.");

                connection = cf.CreateConnection();
                connection.Start();

                ISession producerSession = connection.CreateSession(Constants.SessionMode.AUTO_ACKNOWLEDGE);
                IMessageProducer producer = producerSession.CreateProducer(topic);

                producer.DeliveryMode = Constants.DeliveryMode.PERSISTENT;

                MetricModel model = new MetricModel() { DeviceId = id, Date = date, Value = value };

                ITextMessage jmsMessage = producerSession.CreateTextMessage(JsonConvert.SerializeObject(model));
                producer.Send(jmsMessage);

            }
            catch(Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.MethodNotAllowed);
            }

            connection.Close();
        }

        public DeviceModel RegisterDevice(string deviceType)
        {
            try
            {
                DeviceType type = DeviceTypeDAO.GetByTypeName(deviceType);

                if (type == null)
                    throw new Exception("Invalid Device Type");

                UserData.DAL.Device device = new UserData.DAL.Device()
                {
                    DeviceType = type,
                    DeviceTypeId = type.Id
                };

                var insertedDevice = DeviceDAO.Add(device);

                return new DeviceModel()
                {
                    Id = insertedDevice.Id,
                    DeviceType = insertedDevice.DeviceType.Type
                };
            }
            catch(Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.MethodNotAllowed);
            }
        }
    }
}