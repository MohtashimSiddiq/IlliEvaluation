
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DevExpress.Map;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Map;
using Map.Client.Helpers;
using Map.Client.Interfaces;
using Map.Common.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Unity;
using static Unity.Storage.RegistrationSet;
using DXMap = DevExpress.Xpf.Map;

namespace Map.Client.ViewModels
{
    public class MapViewModel : BindableBase
    {
        MapControl _mapControl;

        VectorLayer soldierLayer;

        internal DXMap.MapItemStorage soldierLayerData = null;

        const string SoldierLayerName = "soldiersLayer";


        EventAggregator _eventAggregator;
        private ILocationProviderService _locationProvider;
        internal const string BingMapKey = "AvyejAJMpeHjZCkhncGdfp1GxwKHicDZxdhcUympxla1pqG3QFl3Mj2Co9EWq5-2";

        private BingMapKind _bingMapKind = BingMapKind.Road;

        private CoordPoint _centerPoint = new GeoPoint(46.95, 7.44);

        public CoordPoint CenterPoint
        {
            get
            {
                return _centerPoint;
            }
            set
            { _centerPoint = value; }
        }

        public CacheOptions MapCacheOptions
        {
            get
            {
                return new CacheOptions()
                {
                    Directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
                    KeepInterval = new TimeSpan(10000, 0, 0, 0, 0)
                };
            }
        }

        public double MinZoomLevel
        {
            get
            {
                return 1;
            }
        }

        public double MaxZoomLevel
        {
            get
            {
                return 20;
            }
        }
        
        private double _zoomLevel = 16;
        public double ZoomLevel
        {
            get
            {
                return _zoomLevel;
            }
            set
            {
                _zoomLevel = value;
            }
        }
        public DelegateCommand<MapControl> MapLoadedCommand { get; }

        /// <summary>
        /// Gets or sets the kind of the bing map.
        /// </summary>
        /// <value>
        /// The kind of the bing map.
        /// </value>
        public BingMapKind BingMapKind
        {
            get => _bingMapKind;
            set
            {
                SetProperty(ref _bingMapKind, value);
                RaisePropertyChanged(nameof(MapSourceText));
            }
        }

        private Soldier _selectedSoldier = new Soldier();

        public Soldier ToolTipInfo { get { return _selectedSoldier; } }
        public object SelectedSoldier
        {
            get { return _selectedSoldier; }
            set
            {
                if (value is DXMap.MapItem mapItem) //case 1:
                {
                    _selectedSoldier = mapItem.Tag as Soldier;                    
                }
                else if (value is Soldier soldier) //case 2:
                {
                    _selectedSoldier = soldier;
                }
                else if (value == null)  // //case 3: Sectors
                {
                    _selectedSoldier = null;
                }
                RaisePropertyChanged(nameof(ToolTipInfo));
            }
        }
        public string PageTitle { get; set; }

        private MapImageDataProviderBase _bingMapProvider;

        private IUnityContainer _container;

        public MapImageDataProviderBase MapProvider
        {
            get
            {

                _bingMapProvider = new BingMapDataProvider() { BingKey = BingMapKey };
                BindingOperations.SetBinding(_bingMapProvider, BingMapDataProvider.KindProperty,new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(BingMapKind)),
                    Mode = BindingMode.OneWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                } );
                return _bingMapProvider;
            }
        }

        public object MapSourceText { get; private set; }
        public ObservableCollection<Soldier> AllSoldiers { get; private set; }

        public MapViewModel(EventAggregator eventAggregator, IUnityContainer container, ILocationProviderService locationProvider)
        {
            _eventAggregator = eventAggregator;
            _locationProvider = locationProvider;
            PageTitle = Application.Current.MainWindow.Resources["txtMap"].ToString();
            MapLoadedCommand = new DelegateCommand<MapControl>(MapLoaded);
            _eventAggregator.GetEvent<SoldierAddedEvent>().Subscribe(SoldierAdded,ThreadOption.UIThread, true);
            _container = container;
            locationProvider.Initialize();
            locationProvider.LocationChanged += LocationProvider_LocationChanged;
            Application.Current.Exit += Current_Exit;
        }

        private void LocationProvider_LocationChanged(object sender, List<LocationInfo> locations)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                soldierLayerData.Items.BeginUpdate();
                foreach (var location in locations)
                {
                    var soldier = AllSoldiers.Where(s => s.DeviceId == location.DeviceId).FirstOrDefault();
                    if (soldier != null)
                    {
                        SoldierUpdated(soldier, location);
                    }
                }
                soldierLayerData.Items.EndUpdate();
            });
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            _locationProvider.UnInitialize();
        }

        private void SoldierAdded(Soldier soldier)
        {
            DXMap.MapPushpin pin  = new DXMap.MapPushpin()
            {
                Tag = soldier,
                Text = soldier.Name,
                Location = new GeoPoint(46.95, 7.44),     
                Brush = new SolidColorBrush(soldier.MarkerColor)
            };
            soldierLayerData.Items.Add(pin);
        }

        private void SoldierUpdated(Soldier soldier, LocationInfo locationInfo)
        {
            var existingSoldier = soldierLayerData.Items.Where(i => (i.Tag as Soldier).DeviceId == soldier.DeviceId).FirstOrDefault();
            if (existingSoldier != null)
            {
                soldierLayerData.Items.Remove(existingSoldier);
            }

            DXMap.MapPushpin pin = new DXMap.MapPushpin()
            {
                Tag = soldier,
                Text = soldier.Name,
                Location = new GeoPoint(locationInfo.Lattitude, locationInfo.Longitude),
                Brush = new SolidColorBrush(soldier.MarkerColor)
            };
            soldierLayerData.Items.Add(pin);
        }


        private void MapLoaded(MapControl control)
        {
            
            _mapControl = control;
            soldierLayer = _mapControl.Layers.Where(layer => layer.Name == SoldierLayerName).FirstOrDefault() as VectorLayer;
            soldierLayerData = soldierLayer?.Data as MapItemStorage;

            if (soldierLayerData != null)
            {
                soldierLayerData.Items.BeginUpdate();
                soldierLayerData.Items.Clear();
                var svm = _container.Resolve<SoldierViewModel>();
                AllSoldiers = svm.AllSoldiers;
                foreach (var soldier in AllSoldiers)
                {
                    SoldierAdded(soldier);
                }
                soldierLayerData.Items.EndUpdate();

            }
            
            RaisePropertyChanged(nameof(CenterPoint));
            RaisePropertyChanged(nameof(ZoomLevel));
        }


    }
}
