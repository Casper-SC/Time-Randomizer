using System;
using System.Globalization;
using System.Windows.Data;

namespace TimeRandomizer.BindingConverters
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    class DateTimeToTimeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("en-US"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
