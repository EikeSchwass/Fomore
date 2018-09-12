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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Equals(value, parameter ?? 0) ? Visibility.Collapsed : Visibility.Visible;

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}