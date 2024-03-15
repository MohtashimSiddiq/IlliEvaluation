using DevExpress.Xpf.Core;
using Map.Client.Interfaces;
using Map.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using ZeroMQ;

namespace Map.Client.Services
{
    public class LocationProviderService : ILocationProviderService
    {
        public event EventHandler<List<LocationInfo>> LocationChanged;
        private ZeroMQ.ZmqContext zmqContext;
        private ZeroMQ.ZmqSocket subscriber;
        private bool _initialized = false;
        private CancellationTokenSource canCellationSource = new CancellationTokenSource();
        private CancellationToken token;
        private ConcurrentQueue<string> MessageQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();
        private System.Timers.Timer timer;
        

        public void Initialize()
        {
            if (!_initialized)
            {
                zmqContext = ZmqContext.Create();
                subscriber = zmqContext.CreateSocket(SocketType.SUB);
                subscriber.Connect("tcp://127.0.0.1:5000");
                subscriber.SubscribeAll();
                token = canCellationSource.Token;
                Thread recieverThread = new Thread(new ThreadStart(RecieveNessage));
                recieverThread.Start();
                timer = new System.Timers.Timer(2000);
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            }
            
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            List<LocationInfo> allLocationMsgs = new List<LocationInfo>();
            while (!MessageQueue.IsEmpty)
            {
                MessageQueue.TryDequeue(out var message);
                if (message is string strMsg)
                {
                    try
                    {
                        var locationInfo = JsonConvert.DeserializeObject<LocationInfo>(strMsg);
                        allLocationMsgs.Add(locationInfo);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            LocationChanged?.Invoke(this, allLocationMsgs);
        }

        private void RecieveNessage()
        {
            while (!token.IsCancellationRequested)
            {
                var message = subscriber.Receive(Encoding.Unicode,
                              new TimeSpan(0, 0, 0, 30));
                if (string.IsNullOrEmpty(message))
                    continue;
                
                MessageQueue.Enqueue(message);
            }
        }

        public void UnInitialize() {
            canCellationSource.Cancel();
            timer.Enabled = false;
            subscriber.Close();
            zmqContext.Terminate();
        }
    }
}
