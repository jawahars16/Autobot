using Autobot.Common;
using Autobot.Platform;
using SQLite;
using System;
using System.Threading.Tasks;

namespace Autobot.Infrastructure
{
    public class Trigger : ISelectable
    {
        public Trigger()
        {
            // Don't kill me. I serve purpose for SQLite.
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
        public string Icon { get; set; }
        public string Id { get; set; }
        public string Rule { get; set; }
        public string Title { get; set; }

        #endregion Serializable

        [Ignore]
        public Type Type { get; set; }

        public static Trigger Create(string id, string title)
        {
            return new Trigger(id, title);
        }

        public static Trigger Create(string title, Type type)
        {
            return new Trigger(title, type);
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