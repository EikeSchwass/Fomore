using System;
using System.Globalization;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    /// <summary>
    ///     Like this
    /// </summary>
    public class InvertedNumberToBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double s = double.Parse(value?.ToString() ?? "0");
            if (double.TryParse(parameter?.ToString(), out double t))
                return !Equals(s, t);
            return !Equals(s, 0.0);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}