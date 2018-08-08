using System;
using System.Globalization;
using System.Windows.Data;

namespace GeekHunterProject
{
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            if (DateTime.TryParse(strValue, out DateTime resultDateTime))
            {
                return resultDateTime;
            }
            return value;
        }
    }
}
