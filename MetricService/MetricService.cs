﻿using Atlantis.RawMetrics.DAL;
using Atlantis.RawMetrics.DAL.Models;
using Atlantis.RawMetrics.Service;
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

        //To keep service alive
        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private Thread _thread;
        
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
            log.Log = ConfigurationManager.AppSettings["EventLogSource"];

            topicHost = ConfigurationManager.AppSettings["topicHost"];
            topicPort = ConfigurationManager.AppSettings["topicPort"];
            connectionFactoryName = ConfigurationManager.AppSettings["connectionFactoryName"];
            topicName = ConfigurationManager.AppSettings["topicName"];

        }

        private void OnMessage(IMessageConsumer sender, MessageEventArgs args)
        {
            ITextMessage msg = (ITextMessage)args.Message;
             
            dynamic rawMetricObject = JsonConvert.DeserializeObject(msg.Text);
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            
            long dateLong = Convert.ToInt64((rawMetricObject.date.Value - epoch).Millisecond); 
            
            Atlantis.RawMetrics.DAL.Models.RawMetric modelMetric = new Atlantis.RawMetrics.DAL.Models.RawMetric()
            {
                Date = dateLong,
                DeviceId = (int)rawMetricObject.deviceId.Value,
                Value = rawMetricObject.value.Value
            };
            RawMetric returnValue;
            returnValue = dao.Create(modelMetric);
            msg.Acknowledge();
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
            

            _thread = new Thread(KeepAlive);
            _thread.Name = "My Worker Thread";
            _thread.IsBackground = true;
            _thread.Start();

        }

        protected override void OnStop()
        {
            log.WriteEntry("Service stopped");
            _shutdownEvent.Set();
            if (!_thread.Join(3000))
            { // give the thread 3 seconds to stop
                _thread.Abort();
            }
        }

        private void KeepAlive()
        {
            while (!_shutdownEvent.WaitOne(0))
            {
                // Replace the Sleep() call with the work you need to do
                Thread.Sleep(1000);
            }
        }
    }

}
