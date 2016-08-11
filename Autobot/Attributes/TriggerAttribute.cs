using System;

namespace Autobot.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public class TriggerAttribute : Attribute
    {
        public string Title { get; set; }
    }
}