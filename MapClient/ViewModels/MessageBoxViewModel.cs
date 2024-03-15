using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Map.Client.Helpers;
using Map.Common.Enums;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Map.Client.ViewModels
{
    public class MessageBoxViewModel:BindableBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public eMessageBoxType Type { get; set; }

        public DelegateCommand OkCommand { get; set; }
        public DelegateCommand YesCommand { get; set; }
        public DelegateCommand NoCommand { get; set; }

        private EventAggregator _eventAggregator;

        public MessageBoxViewModel(EventAggregator eventAggregator)
        {
            InitializeCommands();
            _eventAggregator = eventAggregator;
        }

        private void InitializeCommands()
        {
            OkCommand = new DelegateCommand(OkCommandHandler);
            YesCommand = new DelegateCommand(YesCommandHandler);
            NoCommand = new DelegateCommand(NoCommandHandler);



        }

        private void NoCommandHandler()
        {
            _eventAggregator.GetEvent<MsgBxResultMessageEvent>().Publish(eMessageBoxResult.No);
        }

        private void OkCommandHandler()
        {
            _eventAggregator.GetEvent<MsgBxResultMessageEvent>().Publish(eMessageBoxResult.Ok);
        }

        private void YesCommandHandler()
        {
            _eventAggregator.GetEvent<MsgBxResultMessageEvent>().Publish(eMessageBoxResult.Yes);
        }
    }
}
