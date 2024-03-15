using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Map.Client.Resources.CustomControls
{

    public class ImageTextButton : Button
    {
        static ImageTextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageTextButton), new FrameworkPropertyMetadata(typeof(ImageTextButton)));
        }

        public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register("ImagePath", typeof(ImageSource), typeof(ImageTextButton));

        public ImageSource ImagePath
        {
            get
            {
                return (ImageSource)GetValue(ImagePathProperty);
            }
            set
            {
                SetValue(ImagePathProperty, value);
            }
        }

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(ImageTextButton));

        public string ButtonText
        {
            get
            {
                return (string)GetValue(ButtonTextProperty);
            }
            set
            {
                SetValue(ButtonTextProperty, value);
            }
        }
    }
}
