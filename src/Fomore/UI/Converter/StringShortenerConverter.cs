// : Fomore/UI/StringShortenerConverter.cs (2018/09/15)

using System;
using System.Globalization;
using System.Windows.Data;

namespace Fomore.UI.Converter
{
    public class StringShortenerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            if (value is string message)
            {
                int length = int.Parse(parameter == null ? "0" : parameter.ToString());
                if (message.Length <= length)
                    return message;
                return message.Substring(0, length) + "...";
            }
            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}