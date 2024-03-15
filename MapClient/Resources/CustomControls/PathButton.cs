using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
   
    public class ImageButton : Button
    {
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }


        public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register("ImagePath", typeof(ImageSource), typeof(ImageButton));

        public ImageSource ImagePath
        {
            get
            {
                return (ImageSource)GetValue(ImagePathProperty);
            }
            set
            {
                SetValue(ImagePathProperty,value);
            }
        }

        public static readonly DependencyProperty HoverImagePathProperty = DependencyProperty.Register("HoverImagePath", typeof(ImageSource), typeof(ImageButton));

        public ImageSource HoverImagePath
        {
            get
            {
                return (ImageSource)GetValue(HoverImagePathProperty);
            }
            set
            {
                SetValue(HoverImagePathProperty, value);
            }
        }

        public static readonly DependencyProperty PathVerticalAlignmentProperty = DependencyProperty.Register("PathVerticalAlignment", typeof(VerticalAlignment), typeof(ImageButton));

        public VerticalAlignment PathVerticalAlignment
        {
            get
            {
                return (VerticalAlignment)GetValue(PathVerticalAlignmentProperty);
            }
            set
            {
                SetValue(PathVerticalAlignmentProperty, value);
            }
        }

        public static readonly DependencyProperty PathHorizontalAlignmentProperty = DependencyProperty.Register("PathHorizontalAlignment", typeof(HorizontalAlignment), typeof(ImageButton));

        public HorizontalAlignment PathHorizontalAlignment
        {
            get
            {
                return (HorizontalAlignment) GetValue(PathHorizontalAlignmentProperty);
            }
            set
            {
                SetValue(PathHorizontalAlignmentProperty, value);
            }
        }

        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(ImageButton));

        public double ImageHeight
        {
            get
            {
                return (double)GetValue(ImageHeightProperty);
            }
            set
            {
                SetValue(ImageHeightProperty, value);
            }
        }

        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(ImageButton));

        public double ImageWidth
        {
            get
            {
                return (double)GetValue(ImageWidthProperty);
            }
            set
            {
                SetValue(ImageWidthProperty, value);
            }
        }




    }
}
