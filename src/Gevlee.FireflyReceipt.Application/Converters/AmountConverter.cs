using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Gevlee.FireflyReceipt.Application.Converters
{
    public class AmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is decimal)
            {
                return ((decimal)value).ToString(".00");
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
