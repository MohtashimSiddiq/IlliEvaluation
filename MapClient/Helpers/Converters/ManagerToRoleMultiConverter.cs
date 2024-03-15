using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Map.Client.Helpers.Converters
{
    public class ManagerToStaticStringConverter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
            {
                return null;
            }
            if (values is DependencyObject)
            {
                return Application.Current.MainWindow.Resources["txtVacantPosition"].ToString();
            }
            if (string.IsNullOrEmpty(values.ToString()))
            {
                return Application.Current.MainWindow.Resources["txtVacantPosition"].ToString();
            }
            return values.ToString();
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
