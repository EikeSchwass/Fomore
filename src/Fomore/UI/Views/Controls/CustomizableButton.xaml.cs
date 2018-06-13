using System.Windows;
using System.Windows.Media;
using Fomore.UI.ViewModel.CreatureEditor.Tools;

namespace Fomore.UI.Views.Controls
{
    /// <summary>
    ///     Interaction logic for ToolButton.xaml
    /// </summary>
    public partial class CustomizableButton
    {
        public static readonly DependencyProperty ToolProperty =
            DependencyProperty.Register("Tool", typeof(Tool), typeof(CustomizableButton), new PropertyMetadata(default(Tool)));

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(CustomizableButton), new PropertyMetadata(default(bool)));

        public Tool Tool
        {
            get => (Tool)GetValue(ToolProperty);
            set => SetValue(ToolProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty IsSelectedBackgroundProperty = DependencyProperty.Register("IsSelectedBackground", typeof(Brush), typeof(CustomizableButton), new PropertyMetadata(default(Brush)));

        public Brush IsSelectedBackground
        {
            get { return (Brush)GetValue(IsSelectedBackgroundProperty); }
            set { SetValue(IsSelectedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsMouseOverBackgroundProperty = DependencyProperty.Register("IsMouseOverBackground", typeof(Brush), typeof(CustomizableButton), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty IsPressedBackgroundProperty = DependencyProperty.Register("IsPressedBackground", typeof(Brush), typeof(CustomizableButton), new PropertyMetadata(default(Brush)));

        public Brush IsPressedBackground
        {
            get => (Brush)GetValue(IsPressedBackgroundProperty);
            set => SetValue(IsPressedBackgroundProperty, value);
        }

        public Brush IsMouseOverBackground
        {
            get => (Brush)GetValue(IsMouseOverBackgroundProperty);
            set => SetValue(IsMouseOverBackgroundProperty, value);
        }

        static CustomizableButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomizableButton), new FrameworkPropertyMetadata(typeof(CustomizableButton)));
        }

        public CustomizableButton()
        {
            InitializeComponent();
        }
    }
}