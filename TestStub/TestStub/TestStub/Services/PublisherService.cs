using Map.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TestStub.Interface;
using ZeroMQ;


namespace TestStub.Services
{
    internal class PublisherService : IPublisherService
    {

        private bool _initialized = false;
        private ZeroMQ.ZmqContext zmqContext;
        private ZeroMQ.ZmqSocket publisher;

        public void Initialize()
        {
            if (!_initialized)
            {
                zmqContext = ZmqContext.Create();
                publisher = zmqContext.CreateSocket(SocketType.PUB);
                publisher.Bind("tcp://127.0.0.1:5000");
                _initialized = true;
            }
        }

        public void UnInitialize()
        {
            if (!_initialized) return;
            publisher.Unbind("tcp://127.0.0.1:5000");
            publisher.Close();
            zmqContext.Dispose();
        }

        public void PublishLocation(LocationInfo locationInfo)
        {
            if (!_initialized) return;
            string json = JsonConvert.SerializeObject(locationInfo);
            publisher.Send(json,Encoding.Unicode);

        }



        public void PublishMultipleLocations(List<LocationInfo> locationInfos)
        {

        }
    }
}
