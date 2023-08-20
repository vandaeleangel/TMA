using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TMA.Mobile.Converters
{
    public class DateTimeToStringConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("yyyy-MM-dd"); 
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
