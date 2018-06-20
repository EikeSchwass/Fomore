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
            double targetPosition = (double)values[0];
            double size = (double)values[1];

            return targetPosition - size / 2;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}