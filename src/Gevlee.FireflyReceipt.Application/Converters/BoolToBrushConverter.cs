using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Gevlee.FireflyReceipt.Application.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public string Brush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool && ((bool)value))
            {
                return new BrushConverter().ConvertFromString(Brush);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
