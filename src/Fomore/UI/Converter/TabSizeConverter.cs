// : Fomore/UI/TabSizeConverter.cs (2018/09/15)

using System;
using System.Globalization;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class TabSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = double.Parse(value?.ToString() ?? "0");
            //Subtract 1, otherwise we could overflow to two rows.
            return .25 * width - 5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}