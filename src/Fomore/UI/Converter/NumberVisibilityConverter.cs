// Eike Stein: Fomore/UI/NumberVisibilityConverter.cs (2018/05/12)

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class NumberVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Equals(value, parameter ?? 0) ? Visibility.Visible : Visibility.Collapsed;

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}