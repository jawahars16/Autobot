using Autobot.Common;
using Autobot.Platform;
using SQLite;
using System;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class Trigger : ISelectable
    {
        [PrimaryKey]
        public string PrimaryKey { get; set; }

        public Trigger()
        {
            // Don't kill me. I serve purpose for SQLite.
        }

        public Trigger(string id, string title, int icon, Type type)
        {
            Id = id;
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

        private Trigger(string id, string title)
        {
            Title = title;
            Id = id;
        }

        private Trigger(string title, Type type)
        {
            Title = title;
            Type = type;
        }

        #region Serializable

        public string Description { get; set; }
        public int Icon { get; set; }
        public string Id { get; set; }
        public string Rule { get; set; }
        public string Title { get; set; }
        #endregion Serializable

        [Ignore]
        public bool IsTimeTrigger
        {
            get
            {
                return Id != null && Id.StartsWith(Constants.TIME_TRIGGER);
            }
        }

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
            PrimaryKey = Guid.NewGuid().ToString();
            Rule = rule.PrimaryKey;

            await Database.Default.SaveAsync(this);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}