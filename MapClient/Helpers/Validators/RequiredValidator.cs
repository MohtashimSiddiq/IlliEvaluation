using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Map.Client.Helpers.Validators
{
    class RequiredRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(true, "");
            }
            if (value is string)
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(true, Application.Current.MainWindow.Resources["txtRequiredErr"]);
                }
            }
            return new ValidationResult(true, "");
        }
    }
}
