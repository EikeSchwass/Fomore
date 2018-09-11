// Eike Stein: Fomore/UI/NumberVisibilityConverter.cs (2018/05/12)

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.Converter
{
    /// <summary>
    /// Like this
    /// </summary>
    public class NonEmptyCollectionConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value != null && Equals(((ICollection<MovementPatternVM>) value).Count, 0) ? Visibility.Collapsed : Visibility.Visible;

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}