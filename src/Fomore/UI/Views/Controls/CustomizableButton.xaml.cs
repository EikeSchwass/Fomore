using System.Windows;
using System.Windows.Media;

namespace Fomore.UI.Views.Controls
{
    /// <summary>
    ///     Interaction logic for ToolButton.xaml
    /// </summary>
    public partial class CustomizableButton
    {
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(CustomizableButton), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsDisabledBackgroundProperty =
            DependencyProperty.Register("IsDisabledBackground",
                                        typeof(Brush),
                                        typeof(CustomizableButton),
                                        new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty IsSelectedBackgroundProperty =
            DependencyProperty.Register("IsSelectedBackground",
                                        typeof(Brush),
                                        typeof(CustomizableButton),
                                        new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty IsMouseOverBackgroundProperty =
            DependencyProperty.Register("IsMouseOverBackground",
                                        typeof(Brush),
                                        typeof(CustomizableButton),
                                        new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty IsPressedBackgroundProperty =
            DependencyProperty.Register("IsPressedBackground",
                                        typeof(Brush),
                                        typeof(CustomizableButton),
                                        new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty IsDisabledGrayOpacityProperty =
            DependencyProperty.Register("IsDisabledGrayOpacity",
                                        typeof(double),
                                        typeof(CustomizableButton),
                                        new PropertyMetadata(default(double)));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public double IsDisabledGrayOpacity
        {
            get => (double)GetValue(IsDisabledGrayOpacityProperty);
            set => SetValue(IsDisabledGrayOpacityProperty, value);
        }

        public Brush IsDisabledBackground
        {
            get => (Brush)GetValue(IsDisabledBackgroundProperty);
            set => SetValue(IsDisabledBackgroundProperty, value);
        }

        public Brush IsSelectedBackground
        {
            get => (Brush)GetValue(IsSelectedBackgroundProperty);
            set => SetValue(IsSelectedBackgroundProperty, value);
        }

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