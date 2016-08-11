using System;

namespace Autobot.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ActionAttribute : Attribute
    {
        public string Title { get; set; }
    }
}