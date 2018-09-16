// Dominick Leppich: Fomore/UI/NullVisibilityConverter.cs (2018/07/01)

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class NullBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse(parameter?.ToString(), out bool param);

            return value == null ? param : !param;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}