using System;

namespace Autobot.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public class TriggerAttribute : Attribute
    {
        public int Icon { get; set; }
        public string Title { get; set; }
    }
}