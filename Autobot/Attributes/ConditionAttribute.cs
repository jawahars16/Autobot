using System;

namespace Autobot.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ConditionAttribute : Attribute
    {
        public string Title { get; set; }
    }
}