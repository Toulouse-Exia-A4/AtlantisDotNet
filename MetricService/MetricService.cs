using Atlantis.RawMetrics.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebLogic.Messaging;

namespace MetricService
{
    public partial class MetricService : ServiceBase
    {

        static AutoResetEvent autoEvent = new AutoResetEvent(false);


        RawMetricsDAO dao;
        private string topicHost;
        private string topicPort;
        private string connectionFactoryName;
        private string topicName;

        Timer timer;
        EventLog log;

        public MetricService()
        {
            InitializeComponent();
            log = new EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(ConfigurationManager.AppSettings["EventLogSource"]))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    ConfigurationManager.AppSettings["EventLogSource"], ConfigurationManager.AppSettings["EventLogName"]);
            }
            log.Source = ConfigurationManager.AppSettings["EventLogSource"];
            log.Log = ConfigurationManager.AppSettings["EventLogName"];

            topicHost = ConfigurationManager.AppSettings["topicHost"];
            topicPort = ConfigurationManager.AppSettings["topicPort"];
            connectionFactoryName = ConfigurationManager.AppSettings["connectionFactoryName"];
            topicName = ConfigurationManager.AppSettings["topicName"];

        }

        private void OnMessage(IMessageConsumer sender, MessageEventArgs args)
        {
            log.WriteEntry("Message received :" + args.Message.ToString());
            log.WriteEntry("Object converted :" + JsonConvert.DeserializeObject<Atlantis.RawMetrics.DAL.Models.RawMetric>(args.Message.ToString()));
            dao.Create(JsonConvert.DeserializeObject<Atlantis.RawMetrics.DAL.Models.RawMetric>(args.Message.ToString()));
        }

        protected override void OnStart(string[] args)
        {


            IDictionary<string, Object> paramMap = new Dictionary<string, Object>();

            paramMap[Constants.Context.PROVIDER_URL] =
              "t3://" + topicHost + ":" + topicPort;

            IContext context = ContextFactory.CreateContext(paramMap);


            IConnectionFactory cf = context.LookupConnectionFactory(connectionFactoryName);

            IQueue queue = (IQueue)context.LookupDestination(topicName);

            IConnection connection = cf.CreateConnection();

            connection.ClientID = "MetricService";

            connection.Start();
            ISession consumerSession = connection.CreateSession(
                                Constants.SessionMode.AUTO_ACKNOWLEDGE);

            IMessageConsumer consumer = consumerSession.CreateConsumer(queue);

            consumer.Message += new MessageEventHandler(OnMessage);


            log.WriteEntry("Service started");
            var _context = new RawMetricsContext(true);
            dao = new RawMetricsDAO(_context);

            timer = new Timer(KeepAlive);


        }

        protected override void OnStop()
        {
            log.WriteEntry("Service stopped");
            autoEvent.Set();
        }

        private void KeepAlive(object state)
        {
        }
    }

}
