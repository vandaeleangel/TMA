using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TMA.Mobile.Converters
{
    public class TimeSpanToStringConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan.ToString(@"hh\:mm"); // Format for displaying time span (hh:mm)
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
