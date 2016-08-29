using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Converters;

namespace Autobot.Droid.Converters
{
    public class RadioButtonValueConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _value = (int)value;
            int _parameter = int.Parse(parameter.ToString());

            return _value == _parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool _value = (bool)value;
            int _parameter = int.Parse(parameter.ToString());

            if (_value)
            {
                return _parameter;
            }
            else
            {
                return null;
            }
        }
    }
}