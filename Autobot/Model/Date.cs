using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class Date : ISelectable
    {
        public string Title { get; set; }

        public int Icon { get; set; }

        public DateTime Value { get; set; }

        public static Date Today = Create(DateTime.Today, "Today");

        public static Date Tomorrow = Create(DateTime.Today.AddDays(1), "Tomorrow");

        public static Date NextDay = Create(DateTime.Today.AddDays(7), $"Next {DateTime.Today.DayOfWeek.ToString()}");

        public static Date Custom = Create(default(DateTime), "Pick a date...");

        private Date(DateTime date, string title, int icon)
        {
            Value = date;
            Title = title;
            Icon = icon;
        }

        public static Date Create(DateTime date, string title)
        {
            return new Date(date, title, -1);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
