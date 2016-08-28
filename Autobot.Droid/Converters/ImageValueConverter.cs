using MvvmCross.Platform.Converters;
using System;
using System.Globalization;

namespace Autobot.Droid.Converters
{
    public class ImageValueConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int icon = (int)value;
            if(icon <= 0)
            {
                return Resource.Drawable.place_holder;
            }else
            {
                return icon;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}