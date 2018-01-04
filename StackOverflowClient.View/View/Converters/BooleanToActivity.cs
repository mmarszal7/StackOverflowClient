using System;
using System.Globalization;
using System.Windows.Data;

namespace StackOverflowClient.View.Helpers
{
    public class BooleanToActivity : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "LightGray" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
