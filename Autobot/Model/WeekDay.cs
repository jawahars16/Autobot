using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class WeekDay : ISelectable
    {
        public string Title { get; set; }

        public int Icon { get; set; }

        public DayOfWeek Day { get; set; }

        private WeekDay(DayOfWeek day)
        {
            Day = day;
            Title = day.ToString();
        }

        public static WeekDay Sunday = new WeekDay(DayOfWeek.Sunday);
        public static WeekDay Monday = new WeekDay(DayOfWeek.Monday);
        public static WeekDay Tuesday = new WeekDay(DayOfWeek.Tuesday);
        public static WeekDay Wednesday = new WeekDay(DayOfWeek.Wednesday);
        public static WeekDay Thursday = new WeekDay(DayOfWeek.Thursday);
        public static WeekDay Friday = new WeekDay(DayOfWeek.Friday);
        public static WeekDay Saturday = new WeekDay(DayOfWeek.Saturday);
    }
}
