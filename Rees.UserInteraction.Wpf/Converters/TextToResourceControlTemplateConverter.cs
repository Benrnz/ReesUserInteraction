using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Rees.Wpf.Converters
{
    /// <summary>
    /// Use this converter to take a given string and locate a <see cref="ControlTemplate"/> in the Application resources that matches the given text name.
    /// This is used to display vector (XAML) images instead of used PNG images.
    /// </summary>
    public class TextToResourceControlTemplateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var resourceName = value as string;
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                return null;
            }

            return (ControlTemplate)Application.Current.TryFindResource(resourceName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}