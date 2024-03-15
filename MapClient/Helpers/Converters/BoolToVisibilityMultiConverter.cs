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
    public class BoolToVisibilityMultiConverter : IMultiValueConverter
    {

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;

            bool isButton = false;
            bool showAddBUtton = false;
            Boolean.TryParse(value[0].ToString(), out isButton);
            Boolean.TryParse(value[1].ToString(), out showAddBUtton);

            if (isButton || !showAddBUtton)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
