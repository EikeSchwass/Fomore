using System;
using System.Globalization;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class CenterElementConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double targetPosition = values[0] as double? ?? 0;
            double size = values[1] as double? ?? 0;

            return targetPosition - size / 2;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}