using Map.Client.Helpers;
using Map.Client.Helpers.Enums;
using Map.Client.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using Map.Common;
using Map.Client.Helpers.Enums;
using Map.Common.Enums;
using DevExpress.Xpf.Core;



namespace Map.Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        private MainWindowViewModel _main;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            ChangeResourceDictionary(eLocales.English);
            MapResourceManager.Instance.ChangeTheme(eUIMode.Light, this.Resources.MergedDictionaries);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            _main = (MainWindowViewModel) this.DataContext;
            _main.CurrentLocale = eLocales.English;
            _main.CurrentUIMode = eUIMode.Light;
            if (_main!= null)
            {
                _main.MaximizeEvent += _main_MaximizeEvent;
                _main.MinimizeEvent += _main_MinimizeEvent;
                _main.CloseEvent += _main_CloseEvent;
                _main.ThemeChangedEvent += _main_ThemeChangedEvent;
                _main.LocaleChagedEvent += _main_LocaleChagedEvent;
            }
            _main.UILoadEventHandler();
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void _main_LocaleChagedEvent(object sender, Helpers.RuagEventArgs.LocaleChangeEventArgs e)
        {
            ChangeResourceDictionary(e.NewLocale);
        }


        private void ChangeResourceDictionary(eLocales locale)
        {
            AppLogger.Instance.Log(eLogType.Debug, string.Format("BEGIN:: {0}", System.Reflection.MethodInfo.GetCurrentMethod().Name));
            MapResourceManager.Instance.ChangeLocale(locale, this.Resources.MergedDictionaries);
            
            AppLogger.Instance.Log(eLogType.Debug, string.Format("END:: {0}", System.Reflection.MethodInfo.GetCurrentMethod().Name));

        }
        private void _main_ThemeChangedEvent(object sender, Helpers.RuagEventArgs.ThemeChangeEventArgs e)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            MapResourceManager.Instance.ChangeTheme(e.NewUIMode, this.Resources.MergedDictionaries);
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void _main_CloseEvent(object sender, EventArgs e)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            Application.Current.Shutdown(0);
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void _main_MinimizeEvent(object sender, EventArgs e)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            this.WindowState = WindowState.Minimized;
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void _main_MaximizeEvent(object sender, EventArgs e)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            this.WindowState = WindowState.Maximized;
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        public bool Animate { get; set; }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {

        }



    }
}
