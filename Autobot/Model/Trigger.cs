using Autobot.Common;
using Autobot.Infrastructure.Triggers;
using Autobot.Platform;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class Trigger : ISelectable
    {
        public Trigger()
        {
            // Don't kill me. I serve purpose for SQLite.
        }

        public Trigger(string tag, string title, int icon, Type type)
        {
            Tag = tag;
            Title = title;
            Icon = icon;
            Type = type;
        }

        private Trigger(string title, int icon, Type type)
        {
            Title = title;
            Icon = icon;
            Type = type;
        }

        private Trigger(string tag, string title)
        {
            Title = title;
            Tag = tag;
        }

        private Trigger(string title, Type type)
        {
            Title = title;
            Type = type;
        }

        #region Serializable

        [PrimaryKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public int Icon { get; set; }
        public string Rule { get; set; }
        public long TriggerDelay { get; set; } = -1;
        public long TriggerInterval { get; set; } = -1;

        #endregion Serializable

        [Ignore]
        public bool IsTimeTrigger
        {
            get
            {
                return TriggerInterval > 0;
            }
        }

        [Ignore]
        public TimeSpan TriggerTime { get; set; }

        [Ignore]
        public Type Type { get; set; }

        public static Trigger Create(string id, string title, int icon, Type type)
        {
            return new Trigger(id, title, icon, type);
        }

        public static Trigger Create(string title, int icon, Type type)
        {
            return new Trigger(title, icon, type);
        }

        public async Task SaveAsync(Rule rule)
        {
            if (Tag == TimeTrigger.Default.Custom)
            {
                TriggerDelay = (long)TriggerTime.TotalMilliseconds;
                TriggerInterval = (long)TriggerTime.TotalMilliseconds;
            }
            else if (Tag == TimeTrigger.Default.EveryWeek)
            {

            }
            else if (Tag == TimeTrigger.Default.EveryDay)
            {

            }

            Tag = $"{Constants.TIME_TRIGGER}_{Tag}_{TriggerTime.ToString()}";
            Id = Guid.NewGuid().ToString();
            Rule = rule.Id;

            await Database.Default.SaveAsync(this);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}