using System;
using System.Globalization;
using System.Windows.Data;

namespace PIS8_2.Converters
{
    internal class ConverterNullFileToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vl = value as string;
            if (string.IsNullOrEmpty(vl))
                return "Файл не найден";
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
