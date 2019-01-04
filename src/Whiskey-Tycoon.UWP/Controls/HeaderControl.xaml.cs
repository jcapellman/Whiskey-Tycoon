using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Whiskey_Tycoon.UWP.Controls
{
    public sealed partial class HeaderControl : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
            nameof(HeaderText), typeof(string), typeof(HeaderControl), new PropertyMetadata(default(string)));

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);

            set => SetValue(HeaderTextProperty, value);
        }

        public static readonly DependencyProperty HeaderHelpTextProperty = DependencyProperty.Register(
            nameof(HeaderHelpText), typeof(string), typeof(HeaderControl), new PropertyMetadata(default(string)));

        public string HeaderHelpText
        {
            get => (string)GetValue(HeaderHelpTextProperty);

            set => SetValue(HeaderHelpTextProperty, value);
        }

        public HeaderControl()
        {
            InitializeComponent();

            ((FrameworkElement) Content).DataContext = this;
        }
    }
}