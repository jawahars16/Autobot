using Autobot.Common;
using Autobot.Platform;
using SQLite;
using System;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class Trigger : ISelectable
    {
        public Trigger()
        {
            // Don't kill me. I serve purpose for SQLite.
        }

        public Trigger(string id, string title, int icon)
        {
            Id = id;
            Title = title;
            Icon = icon;
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
        public Type Type { get; set; }

        public static Trigger Create(string id, string title, int icon)
        {
            return new Trigger(id, title, icon);
        }

        public static Trigger Create(string title, int icon, Type type)
        {
            return new Trigger(title, icon, type);
        }

        public async Task SaveAsync(Rule rule)
        {
            Rule = rule.Id;

            await Database.Default.SaveAsync(this);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}