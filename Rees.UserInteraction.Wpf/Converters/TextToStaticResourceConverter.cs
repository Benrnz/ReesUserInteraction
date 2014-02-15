using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Rees.Wpf.Converters
{
    public class TextToStaticResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var resourceName = value as string;
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                return null;
            }

            object returnValue = Application.Current.TryFindResource(resourceName);
            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}