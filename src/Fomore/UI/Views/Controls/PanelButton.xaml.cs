using System.Windows;
using System.Windows.Media;

namespace Fomore.UI.Views.Controls
{
    /// <summary>
    /// Interaction logic for PanelButton.xaml
    /// </summary>
    public partial class PanelButton
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                                        typeof(double),
                                        typeof(PanelButton),
                                        new PropertyMetadata(2.0));

        public static readonly DependencyProperty IsDefaultedBorderBrushProperty =
            DependencyProperty.Register("IsDefaultedBorderBrush",
                                        typeof(Brush),
                                        typeof(PanelButton),
                                        new PropertyMetadata(SystemColors.HighlightBrush));

        public static readonly DependencyProperty IsMouseOverBackgroundBrushProperty =
            DependencyProperty.Register("IsMouseOverBackgroundBrush",
                                        typeof(Brush),
                                        typeof(PanelButton),
                                        new PropertyMetadata(new SolidColorBrush(ParseColorFromHex("FFEBEBEB"))));

        public static readonly DependencyProperty IsMouseOverBorderBrushProperty =
            DependencyProperty.Register("IsMouseOverBorderBrush",
                                        typeof(Brush),
                                        typeof(PanelButton),
                                        new PropertyMetadata(new SolidColorBrush(ParseColorFromHex("FF3C7FB1"))));

        public static readonly DependencyProperty IsPressedBackgroundBrushProperty =
            DependencyProperty.Register("IsPressedBackgroundBrush",
                                        typeof(Brush),
                                        typeof(PanelButton),
                                        new PropertyMetadata(new SolidColorBrush(ParseColorFromHex("FFC4E5F6"))));

        public static readonly DependencyProperty IsPressedBorderBrushProperty =
            DependencyProperty.Register("IsPressedBorderBrush",
                                        typeof(Brush),
                                        typeof(PanelButton),
                                        new PropertyMetadata(new SolidColorBrush(ParseColorFromHex("FF2C628B"))));

        public static readonly DependencyProperty IsDisabledBackgroundBrushProperty =
            DependencyProperty.Register("IsDisabledBackgroundBrush",
                                        typeof(Brush),
                                        typeof(PanelButton),
                                        new PropertyMetadata(new SolidColorBrush(ParseColorFromHex("FFF4F4F4"))));

        public static readonly DependencyProperty IsDisabledBorderBrushProperty =
            DependencyProperty.Register("IsDisabledBorderBrush",
                                        typeof(Brush),
                                        typeof(PanelButton),
                                        new PropertyMetadata(new SolidColorBrush(ParseColorFromHex("FFADB2B5"))));

        public static readonly DependencyProperty IsDisabledForegroundBrushProperty =
            DependencyProperty.Register("IsDisabledForegroundBrush",
                                        typeof(Brush),
                                        typeof(PanelButton),
                                        new PropertyMetadata(new SolidColorBrush(ParseColorFromHex("FF838383"))));

        static PanelButton()
        {
            BackgroundProperty.OverrideMetadata(typeof(PanelButton), new FrameworkPropertyMetadata(Brushes.Black) { Inherits = false });
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PanelButton), new FrameworkPropertyMetadata(typeof(PanelButton)));
        }

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Brush IsDefaultedBorderBrush
        {
            get => (Brush)GetValue(IsDefaultedBorderBrushProperty);
            set => SetValue(IsDefaultedBorderBrushProperty, value);
        }

        public Brush IsMouseOverBackgroundBrush
        {
            get => (Brush)GetValue(IsMouseOverBackgroundBrushProperty);
            set => SetValue(IsMouseOverBackgroundBrushProperty, value);
        }

        public Brush IsMouseOverBorderBrush
        {
            get => (Brush)GetValue(IsMouseOverBorderBrushProperty);
            set => SetValue(IsMouseOverBorderBrushProperty, value);
        }

        public Brush IsPressedBackgroundBrush
        {
            get => (Brush)GetValue(IsPressedBackgroundBrushProperty);
            set => SetValue(IsPressedBackgroundBrushProperty, value);
        }

        public Brush IsPressedBorderBrush
        {
            get => (Brush)GetValue(IsPressedBorderBrushProperty);
            set => SetValue(IsPressedBorderBrushProperty, value);
        }

        public Brush IsDisabledBackgroundBrush
        {
            get => (Brush)GetValue(IsDisabledBackgroundBrushProperty);
            set => SetValue(IsDisabledBackgroundBrushProperty, value);
        }

        public Brush IsDisabledBorderBrush
        {
            get => (Brush)GetValue(IsDisabledBorderBrushProperty);
            set => SetValue(IsDisabledBorderBrushProperty, value);
        }

        public Brush IsDisabledForegroundBrush
        {
            get => (Brush)GetValue(IsDisabledForegroundBrushProperty);
            set => SetValue(IsDisabledForegroundBrushProperty, value);
        }

        private static Color ParseColorFromHex(string hex)
        {
            if (!hex.StartsWith("#"))
                hex = "#" + hex;
            var color = ColorConverter.ConvertFromString(hex);
            return (Color)(color ?? default(Color));
        }

        public PanelButton()
        {
            InitializeComponent();
        }
    }
}
