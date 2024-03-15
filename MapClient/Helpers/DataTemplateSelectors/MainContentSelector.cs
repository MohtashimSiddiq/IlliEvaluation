
using Map.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism.Mvvm;

namespace Map.Client.Helpers.DataTemplateSelectors
{
    public class MainContentSelector : DataTemplateSelector
    {
        public DataTemplate HomeTemplate { get; set; }
        public DataTemplate SettingsTemplate { get; set; }        
        public DataTemplate SoldierTemplate { get; set; }
        public DataTemplate MapTemplate { get; set; }

        public MainContentSelector()
        {
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return HomeTemplate;
            }

            BindableBase vm = (BindableBase)item;
           if (vm is SettingsViewModel)
            {
                return SettingsTemplate;
            }
            else if (vm is SoldierViewModel)
            {
                return SoldierTemplate;
            }
            else if (vm is MapViewModel)
            {
                return MapTemplate;
            }
            else
            {
                return HomeTemplate;
            }
        }
    }
}
