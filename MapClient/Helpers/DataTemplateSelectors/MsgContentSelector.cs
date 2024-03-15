using Map.Client.ViewModels;
using Map.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Map.Client.Helpers.DataTemplateSelectors
{
    public class MsgContentSelector : DataTemplateSelector
    {
        public DataTemplate InfoMsgTemplate { get; set; }
        public DataTemplate ErrorTemplate { get; set; }
        public DataTemplate ConfirmMsgTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            MessageBoxViewModel messageBoxViewModel = item as MessageBoxViewModel;

            if (messageBoxViewModel.Type == eMessageBoxType.Info)
            {
                return InfoMsgTemplate;
            }
            else if (messageBoxViewModel.Type == eMessageBoxType.Error)
            {
                return ErrorTemplate;
            }
            else
            {
                return ConfirmMsgTemplate;
            }

        }
    }
}
