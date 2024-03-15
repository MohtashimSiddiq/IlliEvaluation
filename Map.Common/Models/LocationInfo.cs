using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Common.Models
{
    public class LocationInfo: INotifyPropertyChanged
    {
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
        public int DeviceId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
