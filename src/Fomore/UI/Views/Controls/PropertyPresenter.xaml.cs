using System.Windows;
using System.Windows.Markup;

namespace Fomore.UI.Views.Controls
{
    /// <summary>
    ///     Interaction logic for PropertyPresenter.xaml
    /// </summary>
    [ContentProperty(nameof(PropertyValue))]
    public partial class PropertyPresenter
    {
        public static readonly DependencyProperty PropertyTitleProperty =
            DependencyProperty.Register("PropertyTitle",
                                        typeof(string),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(default(string)));

        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize",
                                        typeof(double),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(default(double)));

        public static readonly DependencyProperty PropertyValueProperty =
            DependencyProperty.Register("PropertyValue",
                                        typeof(string),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ValueFontSizeProperty =
            DependencyProperty.Register("ValueFontSize",
                                        typeof(double),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(default(double)));

        public static readonly DependencyProperty TitleFontWeightProperty =
            DependencyProperty.Register("TitleFontWeight",
                                        typeof(FontWeight),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(FontWeights.Medium));

        public static readonly DependencyProperty ValueFontWeightProperty =
            DependencyProperty.Register("ValueFontWeight",
                                        typeof(FontWeight),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(FontWeights.Normal));

        public static readonly DependencyProperty TitleMarginProperty =
            DependencyProperty.Register("TitleMargin",
                                        typeof(Thickness),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty ValueMarginProperty =
            DependencyProperty.Register("ValueMargin",
                                        typeof(Thickness),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty CanEditValueProperty =
            DependencyProperty.Register("CanEditValue",
                                        typeof(bool),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty TextBoxStyleProperty =
            DependencyProperty.Register("TextBoxStyle",
                                        typeof(Style),
                                        typeof(PropertyPresenter),
                                        new PropertyMetadata(default(Style)));

        public Style TextBoxStyle
        {
            get => (Style)GetValue(TextBoxStyleProperty);
            set => SetValue(TextBoxStyleProperty, value);
        }

        public bool CanEditValue
        {
            get => (bool)GetValue(CanEditValueProperty);
            set => SetValue(CanEditValueProperty, value);
        }

        public Thickness TitleMargin
        {
            get => (Thickness)GetValue(TitleMarginProperty);
            set => SetValue(TitleMarginProperty, value);
        }

        public Thickness ValueMargin
        {
            get => (Thickness)GetValue(ValueMarginProperty);
            set => SetValue(ValueMarginProperty, value);
        }

        public FontWeight TitleFontWeight
        {
            get => (FontWeight)GetValue(TitleFontWeightProperty);
            set => SetValue(TitleFontWeightProperty, value);
        }

        public FontWeight ValueFontWeight
        {
            get => (FontWeight)GetValue(ValueFontWeightProperty);
            set => SetValue(ValueFontWeightProperty, value);
        }

        public string PropertyTitle
        {
            get => (string)GetValue(PropertyTitleProperty);
            set => SetValue(PropertyTitleProperty, value);
        }

        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        public string PropertyValue
        {
            get => (string)GetValue(PropertyValueProperty);
            set => SetValue(PropertyValueProperty, value);
        }

        public double ValueFontSize
        {
            get => (double)GetValue(ValueFontSizeProperty);
            set => SetValue(ValueFontSizeProperty, value);
        }

        public PropertyPresenter()
        {
            InitializeComponent();
        }
    }
}