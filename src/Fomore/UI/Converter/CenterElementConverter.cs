using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class CenterElementConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double targetPosition && values[1] is double size)
            {
                return targetPosition - size / 2;
            }

            Debug.WriteLine($"Invalid Cast @{nameof(CenterElementConverter)}");
            return null;
        }

        /// <inheritdoc />
            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
                throw new NotImplementedException();
        }
    }