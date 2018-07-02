// Dominick Leppich: Fomore/UI/NullVisibilityConverter.cs (2018/07/01)

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public Visibility VisibleOption { get; set; } = Visibility.Visible;
        public Visibility HiddenOption { get; set; } = Visibility.Collapsed;

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? VisibleOption : HiddenOption;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}