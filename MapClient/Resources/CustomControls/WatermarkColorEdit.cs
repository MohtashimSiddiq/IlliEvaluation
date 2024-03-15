using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Map.Client.Resources.CustomControls
{

    public class WatermarkColorEdit : ColorEdit
    {
        static WatermarkColorEdit()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkColorEdit), new FrameworkPropertyMetadata(typeof(WatermarkColorEdit)));
        }

        #region properties

        public static readonly DependencyPropertyKey RemoveWaterMarkKey = DependencyProperty.RegisterReadOnly("RemoveWaterMark", typeof(bool), typeof(WatermarkTextBox), new PropertyMetadata());

        public static readonly DependencyProperty RemoveWaterMarkProperty = RemoveWaterMarkKey.DependencyProperty;

        public bool RemoveWaterMark
        {
            get
            {
                return (bool)GetValue(RemoveWaterMarkProperty);
            }

            set
            {
                SetValue(RemoveWaterMarkProperty, value);
            }
        }


        public static readonly DependencyProperty WaterMarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(WatermarkTextBox));

        public string
            Watermark
        {
            get
            {
                return GetValue(WaterMarkProperty).ToString();
            }
            set
            {
                SetValue(WaterMarkProperty, value);
            }
        }


        #endregion Properties

        protected override void OnColorChanged(Color oldValue, Color newValue)
        {
            if (this.Color != Colors.Transparent)
            {
                this.SetValue(RemoveWaterMarkKey, true);
            }
            else
            {
                this.SetValue(RemoveWaterMarkKey, false);
            }
            base.OnColorChanged(oldValue, newValue);
        }


        protected override void OnGotFocus(RoutedEventArgs e)
        {
            this.SetValue(RemoveWaterMarkKey, true);
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (this.Color != Colors.Transparent)
            {
                this.SetValue(RemoveWaterMarkKey, true);
            }
            else
            {
                this.SetValue(RemoveWaterMarkKey, false);
            }
            base.OnLostFocus(e);
        }
    }


}
