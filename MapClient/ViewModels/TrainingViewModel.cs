using DevExpress.Mvvm;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Client.ViewModels
{
    public class TrainingViewModel: Prism.Mvvm.BindableBase
    {
        EventAggregator _eventAggregator;
        public TrainingViewModel(EventAggregator eventAggregator )
        {
            _eventAggregator = eventAggregator;
        }




    }
}
