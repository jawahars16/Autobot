using Autobot.Attributes;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Trigger(Title = "Manual", Icon = Resource.Drawable.manual)]
    public class ManualTrigger
    {
    }
}