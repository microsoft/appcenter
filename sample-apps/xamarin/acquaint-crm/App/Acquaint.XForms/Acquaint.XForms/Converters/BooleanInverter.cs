using System;
using Xamarin.Forms;
using System.Globalization;

namespace Acquaint.XForms
{
    public class BooleanInverter : IValueConverter
    {
        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
        #endregion

    }
}

