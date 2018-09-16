using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    /// <summary>
    /// Like this
    /// </summary>
    public class InvertedNumberVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double s = double.Parse(value?.ToString() ?? "0");
            if (double.TryParse(parameter?.ToString(), out double t))
                return Equals(s, t) ? Visibility.Collapsed : Visibility.Visible;
            return Equals(s, 0.0) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}