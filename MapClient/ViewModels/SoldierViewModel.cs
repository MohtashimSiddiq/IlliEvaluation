using Newtonsoft.Json;
using Map.Client.Helpers;
using Map.Client.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Threading;
using System.Windows.Media;
using Map.Common;
using Map.Common.Models;
using Map.Common.Enums;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Map.Client.ViewModels
{
    public class SoldierViewModel: BindableBase
    {
        public bool GridDisplaySelected { get; set; }

        public bool _isEdit = false;
        public string PageTitle { get; set; }
        public Soldier _selectedSoldier = new Soldier();
        public Soldier SelectedSoldier
        {
            get { return _selectedSoldier; }
            set
            {
                _selectedSoldier = value;
                RaisePropertyChanged(nameof(SelectedSoldier));
            }
        }

        private EventAggregator _eventAggregator;


        public ObservableCollection<Soldier> AllSoldiers { get; set; } = new ObservableCollection<Soldier>();
       
        #region Commands
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand<object> EditCommand { get; set; }
        public DelegateCommand<object> DeleteCommand { get; set; }
        #endregion
        public SoldierViewModel(EventAggregator eventAggregator)
        {
            InitializeCommands();
            _eventAggregator = eventAggregator;
            GridDisplaySelected = true;
            RaisePropertyChanged("GridDisplaySelected");
            PageTitle = Application.Current.MainWindow.Resources["txtAddSoldier"].ToString();
            RaisePropertyChanged("PageTitle");
            
            SelectedSoldier = new Soldier();
            RaisePropertyChanged(nameof(SelectedSoldier));
            ThreadStart ts = new ThreadStart(FetchData);
            Thread t = new Thread(ts);
            ts.BeginInvoke(null,null);
        }

        private void FetchData()
        {

          
        }
       
        private void InitializeCommands()
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            SaveCommand = new DelegateCommand(SaveCommandHandler);
            EditCommand = new DelegateCommand<object>(EditCommandHandler);
            DeleteCommand = new DelegateCommand<object>(DeleteCommandHandler);
            CancelCommand = new DelegateCommand(CancelCommandHandler);
            AppLogger.Instance.LogEnd(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
        }

        private void EditCommandHandler(object obj)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            try
            {
                SelectedSoldier = null;
                SelectedSoldier = (from emp in AllSoldiers where emp.PersonGuid == Guid.Parse(obj.ToString()) select emp).FirstOrDefault().Clone() as Soldier;
                if (SelectedSoldier != null)
                {
                    _isEdit = true;
                    PageTitle = Application.Current.MainWindow.Resources["txtEditSoldier"].ToString();
                    RaisePropertyChanged(nameof(PageTitle));

                }
                RaisePropertyChanged(nameof(SelectedSoldier));
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

        private void DeleteCommandHandler(object obj)
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            try
            {
                SelectedSoldier = (from emp in AllSoldiers where emp.PersonGuid == Guid.Parse(obj.ToString()) select emp).FirstOrDefault();
                if (SelectedSoldier != null)
                {
                    _eventAggregator.GetEvent<MsgBxResultMessageEvent>().Subscribe(DeleteConfirmResponseHandler);
                    ShowMessageBox(eMessageBoxType.Confirmation, Application.Current.MainWindow.Resources["txtMsgTitleConfirm"].ToString(),
                      Application.Current.MainWindow.Resources["txtMsgTextSolDeleteConfirm"].ToString());
                }
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

        private void DeleteConfirmResponseHandler(eMessageBoxResult obj)
        {
            if (obj == eMessageBoxResult.Yes)
            {
                var currentSoldier = (from sol in AllSoldiers where sol.PersonGuid == SelectedSoldier.PersonGuid select sol).FirstOrDefault();
                if (currentSoldier != null)
                {
                    AllSoldiers.Remove(currentSoldier);
                }
            }         
            SelectedSoldier = new Soldier();
            _eventAggregator.GetEvent<MsgBxResultMessageEvent>().Unsubscribe(DeleteConfirmResponseHandler);
        }
        
        private void CancelCommandHandler()
        {
            _isEdit = false;
            SelectedSoldier = new Soldier();
            
            RaisePropertyChanged(nameof(PageTitle));
        }

        private void SaveCommandHandler()
        {
            AppLogger.Instance.LogBegin(this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            try
            {
                if (!_isEdit)
                {
                    //Add Code
                    //Add code to save to DB
                    SelectedSoldier.PersonGuid = Guid.NewGuid();
                    AllSoldiers.Add(SelectedSoldier.Clone() as Soldier);
                    _eventAggregator.GetEvent<SoldierAddedEvent>().Publish(SelectedSoldier);
                    SelectedSoldier = new Soldier();

                    ShowMessageBox(eMessageBoxType.Info, Application.Current.MainWindow.Resources["txtMsgTitleSuccess"].ToString(),
                        Application.Current.MainWindow.Resources["txtMsgTextSolAddSuccess"].ToString());

                }
                else
                {
                    //Edit Code
                    var currentSoldier = (from sol in AllSoldiers where sol.PersonGuid == SelectedSoldier.PersonGuid select sol).FirstOrDefault();
                    if (currentSoldier != null)
                    {
                        AllSoldiers.Remove(currentSoldier);
                    }

                    SelectedSoldier.PersonGuid = Guid.NewGuid();
                    AllSoldiers.Add(SelectedSoldier.Clone() as Soldier);

                    //if (actionResult.ReturnCode == eReturnCode.Success)
                    {
                        ShowMessageBox(eMessageBoxType.Info, Application.Current.MainWindow.Resources["txtMsgTitleSuccess"].ToString(),
                            Application.Current.MainWindow.Resources["txtMsgTextSolEditSuccess"].ToString());

                    }

                }
                FetchData();

                SelectedSoldier = new Soldier();

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

        private void ShowMessageBox(eMessageBoxType Type, string title, string text)
        {
            _eventAggregator.GetEvent<ShowMsgBxMessageEvent>().Publish(new Tuple<eMessageBoxType, string, string>(Type, title, text));
        }
    }
}
