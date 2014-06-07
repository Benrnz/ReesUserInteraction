using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Rees.Wpf.Converters
{
    public class NotNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringParameter = parameter as string;

            if (stringParameter != null && (stringParameter == string.Empty || stringParameter == "Empty"))
            {
                if (value is string)
                {
                    return string.IsNullOrWhiteSpace(value.ToString()) ? Visibility.Visible : Visibility.Hidden;
                }
            }

            return value == null ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}