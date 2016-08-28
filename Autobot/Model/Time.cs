using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class Time : ISelectable
    {
        public int Icon { get; set; }

        public string Title { get; set; }

        public TimeSpan Value { get; set; }

        public static Time Morning = Create(TimeSpan.FromHours(8), "Morning (8:00 AM)");
        public static Time Noon = Create(TimeSpan.FromHours(13), "Afternoon (1:00 PM)");
        public static Time Evening = Create(TimeSpan.FromHours(18), "Evening (6:00 PM)");
        public static Time Night = Create(TimeSpan.FromHours(18), "Night (10:00 PM)");
        public static Time Custom = Create(default(TimeSpan), "Pick a time...");

        private Time(TimeSpan time, string title, int icon)
        {
            Value = time;
            Title = title;
            Icon = icon;
        }

        public static Time Create(TimeSpan time, string title)
        {
            return new Time(time, title, -1);
        }

        protected Time()
        {

        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
