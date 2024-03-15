using Map.Client.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map.Client.Helpers.Enums;

namespace Map.Client.Helpers.RuagEventArgs
{
    public class LocaleChangeEventArgs:EventArgs
    {
        public eLocales NewLocale { get; set; }
    }


    public class ThemeChangeEventArgs : EventArgs
    {
        public eUIMode NewUIMode { get; set; }
    }
}
