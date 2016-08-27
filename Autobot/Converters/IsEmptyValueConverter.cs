using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using Autobot.Platform;
using System.Linq;

namespace Autobot.Converters
{
    public class IsEmptyValueConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = (IList)value;
            return source.Count <= 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
