using System;
using System.Globalization;
using System.Windows.Data;
using PIS8_2.MVVM.Model.Data;

namespace PIS8_2.Converters
{
    internal class IdToNameConverter:IValueConverter
    {
        private Connection _conn;

        public IdToNameConverter()
        {
            _conn=new Connection();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
