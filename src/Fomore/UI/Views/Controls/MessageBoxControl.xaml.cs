using System.Windows;

namespace Fomore.UI.Views.Controls
{
    /// <summary>
    ///     Interaction logic for MessageBoxControl.xaml
    /// </summary>
    public partial class MessageBoxControl
    {
        public static readonly DependencyProperty MessageBoxParametersProperty =
            DependencyProperty.Register("MessageBoxParameters",
                                        typeof(MessageBoxParameters),
                                        typeof(MessageBoxControl),
                                        new PropertyMetadata(default(MessageBoxParameters)));
        
        public MessageBoxParameters MessageBoxParameters
        {
            get => (MessageBoxParameters)GetValue(MessageBoxParametersProperty);
            set => SetValue(MessageBoxParametersProperty, value);
        }

        public MessageBoxControl()
        {
            InitializeComponent();
        }
    }
}