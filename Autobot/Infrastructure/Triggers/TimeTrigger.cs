using Autobot.Attributes;

namespace Autobot.Infrastructure.Triggers
{
    public class TimeTrigger
    {
        [Trigger(Title = "Every day")]
        public string EveryDay = "EVERY_DAY";

        [Trigger(Title = "Every week")]
        public string EveryWeek = "EVERY_WEEK";

        [Trigger(Title = "Custom...")]
        public string Custom = "CUSTOM";

        public static TimeTrigger Default { get; set; } = new TimeTrigger();
    }
}