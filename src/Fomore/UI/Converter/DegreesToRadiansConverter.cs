using System;
using System.Globalization;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class DegreesToRadiansConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value?.ToString() ?? "0", out double angle)) return angle / 180.0 * Math.PI;
            throw new InvalidOperationException("Must be double to invert");
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value?.ToString() ?? "0", out double angle)) return angle * 180 / Math.PI;
            throw new InvalidOperationException("Must be double to invert");
        }
    }
}