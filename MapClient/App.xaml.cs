using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Map.Client.Interfaces;
using Map.Client.Services;
using Map.Client.ViewModels;
using Prism.Events;
using Prism.Ioc;
using Prism.Unity;

namespace Map.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<MapViewModel>();
            containerRegistry.RegisterSingleton<SoldierViewModel>();
            containerRegistry.RegisterSingleton<ILocationProviderService,LocationProviderService>();
        }

        protected override Window CreateShell()
        {
            Views.MainWindow window = new Views.MainWindow();
            //window.DataContext = new ViewModel.MainWindowViewModel(Container.GetContainer().Resolve<EventAggregator>());
            return window;
        }
    }
}
