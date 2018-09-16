using System;
using System.Globalization;
using System.Windows.Data;

namespace StackOverflowClient.View.Helpers
{
    public class EpochToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int unixSeconds = Int32.Parse(value.ToString());
            DateTime creationDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return creationDate.AddSeconds(unixSeconds).ToString(@"MMM dd \'yy 'at' hh:mm", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = DateTime.ParseExact(value.ToString(), @"MMM dd \'yy 'at' hh:mm", CultureInfo.InvariantCulture);
            TimeSpan t = date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            return secondsSinceEpoch;
        }
    }
}
