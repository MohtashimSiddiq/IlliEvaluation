
using System;
using Microsoft.Windows.Design.Interaction;
using System.Threading;
using Map.Client.Helpers;
using Map.Client.Helpers.Enums;
using Map.Client.Helpers.RuagEventArgs;
using Map.Common;
using Map.Common.Enums;
using Map.Client.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Unity;
using System.Windows;

namespace Map.Client.ViewModels
{
    
    public class MainWindowViewModel : BindableBase
    {
        public eUIMode CurrentUIMode { get; set; }
        public eLocales CurrentLocale { get; set; }

        private UIScreens _selectedScreen;
        public BindableBase MainContent { get; set; }
        public BindableBase MsgBxContent { get; set; }
        public bool ShowMsgBox { get; set; }

        private EventAggregator _eventAggregator;
        private IUnityContainer _container;

        public UIScreens SelectedScreen
        {
            get { return _selectedScreen; } 
            set
            {
                if (value == _selectedScreen)
                {
                    return;
                }
                _selectedScreen = value;
                ChangeScreen(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainWindowViewModel(EventAggregator eventAggregator, IUnityContainer container)
        {
            InitializeCommands();
            _eventAggregator = eventAggregator;
            RegisterEventHandlers();
            MainContent = new HomeViewModel();
            SelectedScreen = UIScreens.Home;
            RaisePropertyChanged("SelectedScreen");
            ShowMsgBox = false;
            RaisePropertyChanged("ShowMsgBox");
            _container = container;
            
        }

        

        private void RegisterEventHandlers()
        {
            _eventAggregator.GetEvent<LocaleMessageEvent>().Subscribe(LocaleChangeMsgHandler);
            _eventAggregator.GetEvent<UIModeMessageEvent>().Subscribe(UIModeChangeMsgHandler);
            _eventAggregator.GetEvent<ShowMsgBxMessageEvent>().Subscribe(ShowMessageBxMsgHandler);
            
        }

        private void ShowMessageBxMsgHandler(Tuple<eMessageBoxType, string, string> obj)
        {
            //MessageBoxViewModel vm_MsgBox = new MessageBoxViewModel() { Type = obj.Type, Title = obj.Title, Text = obj.Text };
            MsgBxContent = new MessageBoxViewModel(_eventAggregator) { Type = obj.Item1, Title = obj.Item2, Text = obj.Item3};
            ShowMsgBox = true;

            RaisePropertyChanged(nameof(MsgBxContent));
            RaisePropertyChanged(nameof(ShowMsgBox));
        }

        #region Events

        public event EventHandler MinimizeEvent;
        public event EventHandler MaximizeEvent;
        public event EventHandler CloseEvent;
        public event EventHandler<LocaleChangeEventArgs> LocaleChagedEvent;
        public event EventHandler<ThemeChangeEventArgs> ThemeChangedEvent;
        
        #endregion

        #region Commands
        public DelegateCommand MinimizeCommand { get; set; }
        public DelegateCommand MaximizeCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
       
        #endregion


        public void UILoadEventHandler()
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            try
            {
                MainContent = new HomeViewModel();
                RaisePropertyChanged("ContentViewModel");
            }
            catch (Exception ex)
            {
                AppLogger.Instance.Log(eLogType.Error, ex.ToString());
            }
            finally
            {
                AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
           
        }

        private void InitializeCommands()
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            try
            {
                MinimizeCommand = new DelegateCommand(MinimizeCommandHandler);
                MaximizeCommand = new DelegateCommand(MaximizeCommandHandler);
                CloseCommand = new DelegateCommand(CloseCommandHandler);
            }
            catch (Exception ex)
            {
                AppLogger.Instance.Log(eLogType.Error, ex.ToString());
            }
            finally
            {
                AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
        }

        private void ChangeScreen(UIScreens screen)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            switch (screen)
            {
                default:
                case UIScreens.Home:
                    MainContent = new HomeViewModel();
                    break;
                case UIScreens.Trainings:
                    MainContent = new TrainingViewModel(_eventAggregator);
                    break;
                case UIScreens.Soldiers:
                    MainContent = _container.Resolve< SoldierViewModel >();
                    break;
                case UIScreens.Map:
                    MainContent = _container.Resolve<MapViewModel>();
                    break;
                case UIScreens.Settings:
                    SettingsViewModel Vm_Settings = new SettingsViewModel(_eventAggregator) {SelectedUIMode = CurrentUIMode, SelectedLocale = CurrentLocale };
                    MainContent = Vm_Settings;
                    break;
                
            }
            RaisePropertyChanged("MainContent");
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        


        private void CloseCommandHandler()
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            if (CloseEvent != null)
            {
                CloseEvent(this, null);
            }
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void MaximizeCommandHandler()
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            if (MaximizeEvent != null)
            {
                MaximizeEvent(this, null);
            }
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void MinimizeCommandHandler()
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            if (MinimizeEvent != null)
            {
                MinimizeEvent(this, null);
            }
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void UIModeChangeMsgHandler(eUIMode obj)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            CurrentUIMode = obj;
            ThemeChangedEvent(this, new ThemeChangeEventArgs() { NewUIMode = obj });
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }


        private void LocaleChangeMsgHandler(eLocales obj)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            CurrentLocale = obj;
            LocaleChagedEvent(this, new LocaleChangeEventArgs() { NewLocale = obj });
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }
    }
}