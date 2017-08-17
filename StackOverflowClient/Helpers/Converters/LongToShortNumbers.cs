using System;
using System.Globalization;
using System.Windows.Data;

namespace StackOverflowClient.Helpers
{
    public class LongToShortNumbers : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = Int32.Parse(value.ToString());
            if (number < 1000)
                return value;
            else if (number < 1000000)
                return (number / 1000).ToString() + "k";
            else
                return Math.Round((double)number / 1000000, 1).ToString() + "m";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tempString = value.ToString();

            int number = Int32.Parse(tempString.Substring(0, tempString.Length - 2));
            char lastChar = tempString[tempString.Length - 1];

            if (lastChar.Equals("k"))
                return number * 1000;
            if (lastChar.Equals("m"))
                return number * 1000000;
            else return value;
        }
    }
}
