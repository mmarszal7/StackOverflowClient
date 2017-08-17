using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace StackOverflowClient.Helpers
{
    public class HtmlToPlainText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(value.ToString(), "");
            string clearContent = System.Net.WebUtility.HtmlDecode(stripped);

            return clearContent.Length > 370 ? clearContent.Substring(0, 370) + "..." : clearContent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
