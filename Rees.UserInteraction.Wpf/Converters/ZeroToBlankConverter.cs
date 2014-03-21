﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Rees.Wpf.Converters
{
    /// <summary>
    /// A Converter that will convert any type (decimal, double, int etc) of zero into Null and leave non-zero intact.
    /// </summary>
    public class ZeroToBlankConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is decimal && (decimal) value == 0)
            {
                return null;
            }

            if (value is double && (double) value == 0)
            {
                return null;
            }

            if (value is int && (int) value == 0)
            {
                return null;
            }

            if (value is long && (long) value == 0)
            {
                return null;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}