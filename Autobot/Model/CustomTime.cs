using Autobot.Common;
using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class CustomTime : Time
    {
        private CustomTime(TimeSpan value)
        {
            Value = value;
            Title = $"Every {value.ToHumanReadableString()}";
        }

        public static CustomTime EVERY_5_SECONDS = new CustomTime(TimeSpan.FromSeconds(5));
        public static CustomTime EVERY_30_MINUTES = new CustomTime(TimeSpan.FromMinutes(30));
        public static CustomTime EVERY_ONE_HOUR = new CustomTime(TimeSpan.FromHours(1));
        public static CustomTime EVERY_4_HOURS = new CustomTime(TimeSpan.FromHours(4));
        public static CustomTime EVERY_8_HOURS = new CustomTime(TimeSpan.FromHours(8));
        public static CustomTime EVERY_2_DAYS = new CustomTime(TimeSpan.FromDays(2));
        public static CustomTime EVERY_10_DAYS = new CustomTime(TimeSpan.FromDays(10));
        public static CustomTime EVERY_20_DAYS = new CustomTime(TimeSpan.FromDays(20));
    }
}
