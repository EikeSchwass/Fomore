using System.Windows;
using System.Windows.Media;

namespace Fomore.UI.Views.Controls
{
    /// <summary>
    ///     Interaction logic for BoxContentPresenter.xaml
    /// </summary>
    public partial class BoxContentPresenter
    {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                                        typeof(string),
                                        typeof(BoxContentPresenter),
                                        new PropertyMetadata("Untitled"));

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize",
                                        typeof(double),
                                        typeof(BoxContentPresenter),
                                        new PropertyMetadata(default(double)));

        public static readonly DependencyProperty HeaderFontMarginProperty =
            DependencyProperty.Register("HeaderFontMargin",
                                        typeof(Thickness),
                                        typeof(BoxContentPresenter),
                                        new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground",
                                        typeof(Brush),
                                        typeof(BoxContentPresenter),
                                        new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty ContentBackgroundProperty =
            DependencyProperty.Register("ContentBackground",
                                        typeof(Brush),
                                        typeof(BoxContentPresenter),
                                        new PropertyMetadata(default(Brush)));

        public Brush HeaderBackground
        {
            get => (Brush)GetValue(HeaderBackgroundProperty);
            set => SetValue(HeaderBackgroundProperty, value);
        }

        public Brush ContentBackground
        {
            get => (Brush)GetValue(ContentBackgroundProperty);
            set => SetValue(ContentBackgroundProperty, value);
        }

        public double HeaderFontSize
        {
            get => (double)GetValue(HeaderFontSizeProperty);
            set => SetValue(HeaderFontSizeProperty, value);
        }

        public Thickness HeaderFontMargin
        {
            get => (Thickness)GetValue(HeaderFontMarginProperty);
            set => SetValue(HeaderFontMarginProperty, value);
        }

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public BoxContentPresenter()
        {
            InitializeComponent();
        }
    }
}