using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Finance
{
    public class EnumBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string paramStr = parameter as string;
            object parameterValue = DependencyProperty.UnsetValue;
            if (paramStr != null)
            {
                parameterValue = Enum.Parse(value.GetType(), paramStr);
            }
            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object obj = DependencyProperty.UnsetValue;
            string paramStr = parameter as string;
            if (paramStr != null)
            {
                obj = Enum.Parse(targetType, paramStr);
            }
            return obj;
        }
    }
}
