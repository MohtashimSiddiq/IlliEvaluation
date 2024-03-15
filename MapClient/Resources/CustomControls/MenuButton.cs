using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Map.Client.Resources.CustomControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Map.Client.Resources.CustomControls;assembly=Map.Client.Resources.CustomControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:MenuButton/>
    ///
    /// </summary>
    public class MenuRadioButton : RadioButton
    {
        static MenuRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuRadioButton), new FrameworkPropertyMetadata(typeof(MenuRadioButton)));
        }

        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            "RadioValue",
            typeof(object),
            typeof(MenuRadioButton),
            new UIPropertyMetadata(null));

        public SolidColorBrush ForeColor
        {
            get { return (SolidColorBrush)GetValue(ForeColorProperty); }
            set { SetValue(ForeColorProperty, value); }
        }


        public static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register(
                "ForeColor",
                typeof(SolidColorBrush),
                typeof(MenuRadioButton),
                new UIPropertyMetadata(null));

        public SolidColorBrush HoverColor
        {
            get { return (SolidColorBrush)GetValue(HoverColorProperty); }
            set { SetValue(HoverColorProperty, value); }
        }


        public static readonly DependencyProperty HoverColorProperty =
            DependencyProperty.Register(
                "HoverColor",
                typeof(SolidColorBrush),
                typeof(MenuRadioButton),
                new UIPropertyMetadata(null));

        public object RadioBinding
        {
            get { return (object)GetValue(RadioBindingProperty); }
            set { SetValue(RadioBindingProperty, value); }
        }

        
    public static readonly DependencyProperty RadioBindingProperty =
        DependencyProperty.Register(
            "RadioBinding",
            typeof(object),
            typeof(MenuRadioButton),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnRadioBindingChanged));

        private static void OnRadioBindingChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            MenuRadioButton rb = (MenuRadioButton)d;


            if (rb.Value.Equals(e.NewValue))
                rb.SetCurrentValue(RadioButton.IsCheckedProperty, true);
            else
                rb.SetCurrentValue(RadioButton.IsCheckedProperty, false);
        }

        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            SetCurrentValue(RadioBindingProperty, Value);
        }
    }
}
