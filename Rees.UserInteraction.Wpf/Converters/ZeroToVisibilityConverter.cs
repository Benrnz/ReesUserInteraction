using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Rees.Wpf.Converters
{
    /// <summary>
    /// A Converter that will convert any type (decimal, double, int etc) of zero into <see cref="Visibility.Collapsed"/> and 
    /// non-zero into <see cref="Visibility.Visible"/>.
    /// </summary>
    public class ZeroToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Hidden;
            }

            if (value is decimal && (decimal) value == 0)
            {
                return Visibility.Hidden;
            }

            if (value is double && (double) value == 0)
            {
                return Visibility.Hidden;
            }

            if (value is int && (int) value == 0)
            {
                return Visibility.Hidden;
            }

            if (value is long && (long) value == 0)
            {
                return Visibility.Hidden;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}