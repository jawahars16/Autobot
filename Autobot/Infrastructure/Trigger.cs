using Autobot.Platform;
using System;

namespace Autobot.Infrastructure
{
    public class Trigger : ISelectable
    {
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

        public string Icon { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public Type Type { get; set; }

        public static Trigger Create(string id, string title)
        {
            return new Trigger(id, title);
        }

        public static Trigger Create(string title, Type type)
        {
            return new Trigger(title, type);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}