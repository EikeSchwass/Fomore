// Eike Stein: Fomore/UI/BooleanVisiblityConverter.cs (2018/05/13)

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class BooleanVisibilityConverter : IValueConverter
    {
        public Visibility VisibleOption { get; set; } = Visibility.Visible;
        public Visibility HiddenOption { get; set; } = Visibility.Collapsed;

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse(parameter?.ToString(), out bool param);

            if (value is bool source)
                return source == param ? VisibleOption : HiddenOption;
            throw new InvalidOperationException($"{nameof(value)} is not a boolean value");
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}