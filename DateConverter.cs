using System;
using System.Globalization;
using System.Windows.Data;

namespace GeekHunterProject
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var strValue = value.ToString();
                if (DateTime.TryParse(strValue, out var resultDateTime)) return resultDateTime;
            }

            return value;
        }
    }
}
