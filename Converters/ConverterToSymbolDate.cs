using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PIS8_2.Converters
{
    internal class ConverterToSymbolDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("D", CultureInfo.CreateSpecificCulture("ru-RU"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
