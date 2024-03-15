using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using TestStub.Interface;
using TestStub.Services;
using TestStub.Views;

namespace TestStub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPublisherService, PublisherService>();
        }

        protected override Window CreateShell()
        {
            return new Views.MainWindow();
        }
    }
}
