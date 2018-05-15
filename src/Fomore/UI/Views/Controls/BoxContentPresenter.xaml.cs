using System.Windows;
using System.Windows.Media;

namespace Fomore.UI.Views.Controls
{
    /// <summary>
    ///     Interaction logic for BoxContentPresenter.xaml
    /// </summary>
    public partial class BoxContentPresenter
    {
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent",
                                        typeof(object),
                                        typeof(BoxContentPresenter),
                                        new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize",
                                        typeof(double),
                                        typeof(BoxContentPresenter),
                                        new PropertyMetadata(default(double)));

        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.Register("HeaderMargin",
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

        public Thickness HeaderMargin
        {
            get => (Thickness)GetValue(HeaderMarginProperty);
            set => SetValue(HeaderMarginProperty, value);
        }

        public object HeaderContent
        {
            get => GetValue(HeaderContentProperty);
            set => SetValue(HeaderContentProperty, value);
        }

        public BoxContentPresenter()
        {
            InitializeComponent();
        }
    }
}