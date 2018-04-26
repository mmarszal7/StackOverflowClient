using System;
using System.Globalization;
using System.Windows.Data;

namespace StackOverflowClient.View.Helpers
{
    public class BooleanToVisability : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? "Visible" : "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == "Visible" ? true : false;
        }
    }
}
