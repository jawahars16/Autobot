using Android.Content;
using Autobot.Attributes;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Trigger(Title = "Display", Icon = Resource.Drawable.display)]
    public class DisplayTrigger
    {
        [Trigger(Title = "On Screen ON", Icon = Resource.Drawable.screen_on)]
        public string OnScreenON = Intent.ActionScreenOn;

        [Trigger(Title = "On Screen OFF", Icon = Resource.Drawable.screen_off)]
        public string OnScreenOFF = Intent.ActionScreenOn;

        [Trigger(Title = "On Dreaming started", Icon = Resource.Drawable.dreaming_started)]
        public string OnDreamingStarted = Intent.ActionDreamingStarted;

        [Trigger(Title = "On Dreaming stopped")]
        public string OnDreamingStopped = Intent.ActionDreamingStopped;
    }
}