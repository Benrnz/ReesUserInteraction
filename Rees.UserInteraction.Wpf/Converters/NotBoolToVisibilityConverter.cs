using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Rees.Wpf.Converters
{
    /// <summary>
    /// Use when you'd prefer hide something leaving a blank space for it rather than collapsing the space when using the 
    /// standard <see cref="BooleanToVisibilityConverter"/>.
    /// </summary>
    public class NotBoolToVisibilityConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Collapsed: Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
