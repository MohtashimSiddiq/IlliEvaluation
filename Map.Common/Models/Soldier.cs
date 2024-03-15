using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Map.Common.Models
{
    public class Soldier: INotifyPropertyChanged, ICloneable
    {
        public Guid PersonGuid { get; set; }
        public string Name { get; set; }
        public int DeviceId { get; set; }
        public string Rank { get; set; }
        public string Country { get; set; }
        public string Training { get; set; }
        public Color MarkerColor { get; set; } = new Color();

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return new Soldier() { PersonGuid = Guid.NewGuid(), Country = this.Country, Name = this.Name, Training = this.Training, MarkerColor   = this.MarkerColor, Rank = this.Rank, DeviceId = this.DeviceId };
        }
    }
}
