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
                if(timeSpan.Days > 0)
                {
                    double totalHours = timeSpan.TotalHours;
                    int days = (int)Math.Floor(totalHours / 24);
                    int remainingHours = (int)(totalHours % 24);
                    int minutes = timeSpan.Minutes;

                    TimeSpan formattedTimeSpan = new TimeSpan(days, remainingHours, minutes, 0);

                    return formattedTimeSpan.ToString(@"hh\:mm");
                }
                else
                {
                    return timeSpan.ToString(@"hh\:mm"); 
                }
               
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
