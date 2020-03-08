using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.IO;

namespace Gevlee.FireflyReceipt.Application.Converters
{
    public class FileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is String)
            {
                return Path.GetFileName(value as String);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
