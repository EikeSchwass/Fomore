using System;
using System.Globalization;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class InvertBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (bool.TryParse(value?.ToString() ?? "false", out bool v))
            {
                return !v;
            }
            throw new InvalidOperationException("Must be bool to invert");
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (bool.TryParse(value?.ToString() ?? "", out bool v))
            {
                return !v;
            }
            throw new InvalidOperationException("Must be bool to invert");
        }
    }
}