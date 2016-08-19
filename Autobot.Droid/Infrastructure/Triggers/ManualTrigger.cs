using Autobot.Attributes;
using Autobot.Common;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Trigger(Title = "Manual", Icon = Resource.Drawable.manual)]
    public class ManualTrigger
    {
        [Trigger(Title = "On Manual trigger")]
        public string OnManual = "MANUAL" + Constants.TRIGGER_DELIMITER + "TRIGGER";

    }
}