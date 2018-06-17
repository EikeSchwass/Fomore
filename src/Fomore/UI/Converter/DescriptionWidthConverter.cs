using System;
using System.Globalization;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class DescriptionWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return (double)value - 110;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
