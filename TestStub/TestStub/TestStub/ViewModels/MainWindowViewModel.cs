using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Map.Common.Models;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using TestStub.Interface;
using TestStub.Services;

namespace TestStub.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        private bool _isEmulating;

        private IPublisherService _publisherService;
        public int SelectedDevices { get; set; }
        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }
        public DelegateCommand ClearCommand { get; set; }

        private CancellationTokenSource canCellationSource = new CancellationTokenSource();

        private CancellationToken token;

        private double BaseLattitude = 46.95;

        private double BaseLongitude = 7.44;

        private Random _random = new Random();

        public  MainWindowViewModel(IPublisherService publisherService)
        {
            _publisherService = publisherService;
            StartCommand = new DelegateCommand(Start);
            StopCommand = new DelegateCommand(Stop);
            ClearCommand = new DelegateCommand(Clear);
            Application.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            _publisherService.UnInitialize();
        }

        private void Stop()
        {
            canCellationSource.Cancel();
        }

        private void Start()
        {
            canCellationSource = new CancellationTokenSource();
            token = canCellationSource.Token;   
            _publisherService.Initialize();
            Thread recieverThread = new Thread(new ThreadStart(PublishLocations));
            recieverThread.Start();


        }

        private void PublishLocations()
        {
            while (!token.IsCancellationRequested)
            {
                for(int i = 1; i <= SelectedDevices; i++)
                {
                    var locationInfo  = GetRandomLocation(i);
                    _publisherService.PublishLocation(locationInfo);
                }
                Thread.Sleep(3000);  
            }
        }

        private LocationInfo GetRandomLocation(int deviceId)
        {
            LocationInfo locationInfo = new LocationInfo();
            locationInfo.Lattitude =  BaseLattitude + (_random.Next(1, 1000) / 1000F);
            locationInfo.Longitude = BaseLongitude + (_random.Next(1, 1000) / 1000F);
            locationInfo.DeviceId = deviceId;
            return locationInfo;
        }

        private void Clear()
        {
           
        }
    }
}
