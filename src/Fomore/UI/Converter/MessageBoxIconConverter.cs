// Eike Stein: Fomore/UI/MessageBoxIconConverter.cs (2018/05/16)

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using FontAwesome.WPF;

namespace Fomore.UI.Converter
{
    public class MessageBoxIconConverter : IValueConverter
    {
        private FontAwesomeIcon Convert(MessageBoxImage messageBoxImage)
        {
            switch (messageBoxImage)
            {
                case MessageBoxImage.None:
                    return FontAwesomeIcon.None;
                case MessageBoxImage.Hand:
                    return FontAwesomeIcon.StopCircle;
                case MessageBoxImage.Question:
                    return FontAwesomeIcon.QuestionCircle;
                case MessageBoxImage.Exclamation:
                    return FontAwesomeIcon.ExclamationCircle;
                case MessageBoxImage.Information:
                    return FontAwesomeIcon.InfoCircle;
                default:
                    throw new ArgumentOutOfRangeException(nameof(messageBoxImage), messageBoxImage, null);
            }
        }

        private MessageBoxImage Convert(FontAwesomeIcon fontAwesomeIcon)
        {
            switch (fontAwesomeIcon)
            {
                case FontAwesomeIcon.None: return MessageBoxImage.None;
                case FontAwesomeIcon.StopCircle: return MessageBoxImage.Hand;
                case FontAwesomeIcon.QuestionCircle: return MessageBoxImage.Question;
                case FontAwesomeIcon.ExclamationCircle: return MessageBoxImage.Exclamation;
                case FontAwesomeIcon.InfoCircle: return MessageBoxImage.Information;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fontAwesomeIcon), fontAwesomeIcon, null);
            }
        }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MessageBoxImage messageBoxImage)
                return Convert(messageBoxImage);
            if (value is FontAwesomeIcon fontAwesomeIcon)
                return Convert(fontAwesomeIcon);
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MessageBoxImage messageBoxImage)
                return Convert(messageBoxImage);
            if (value is FontAwesomeIcon fontAwesomeIcon)
                return Convert(fontAwesomeIcon);
            throw new NotSupportedException();
        }
    }
}