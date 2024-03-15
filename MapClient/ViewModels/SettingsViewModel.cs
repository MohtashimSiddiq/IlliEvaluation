using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Map.Client.Helpers;
using Map.Client.Helpers.Enums;
using Map.Common;
using Prism.Events;
using Prism.Mvvm;

namespace Map.Client.ViewModels
{
    public class SettingsViewModel:BindableBase
    {

        private eUIMode _selectedUIMode;
        public eUIMode SelectedUIMode
        {
            get
            {
                return _selectedUIMode;
            }

            set
            {
                if (value != _selectedUIMode)
                {
                    ModeChanged(value);
                }
                _selectedUIMode = value;
                
            }

        }

        private EventAggregator _eventAggregator;

        private eLocales _selectedLocale { get; set; }
        public eLocales SelectedLocale
        {
            get
            {
                return _selectedLocale;
            }

            set
            {
                if (value != _selectedLocale)
                {
                    LocaleChanged(value);
                }
                _selectedLocale = value;
              
            }

        }

        public SettingsViewModel(EventAggregator eventAggregator)
        {
            InitializeCommands();
            _eventAggregator = eventAggregator;
        }

        private void InitializeCommands()
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void LocaleChanged(eLocales locale)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            _eventAggregator.GetEvent<LocaleMessageEvent>().Publish(locale); ;
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void ModeChanged(eUIMode uIMode)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            _eventAggregator.GetEvent<UIModeMessageEvent>().Publish(uIMode);
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void ColorChangedCommandHandler()
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            //_eventAggregator.GetEvent<UIColorMessage>().Publish();
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }
    }
}
