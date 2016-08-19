using Android.Content;
using Autobot.Attributes;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Trigger(Title = "Battery", Icon = Resource.Drawable.battery)]
    public class BatteryTrigger
    {
        [Trigger(Title = "On Battery Low", Icon = Resource.Drawable.battery_low)]
        public string OnBatteryLow = Intent.ActionBatteryLow;

        [Trigger(Title = "On Battery Okay", Icon = Resource.Drawable.battery_okay)]
        public string OnBatteryOkay = Intent.ActionBatteryOkay;

        [Trigger(Title = "On Charger connected", Icon = Resource.Drawable.power_connected)]
        public string OnPowerConnected = Intent.ActionPowerConnected;

        [Trigger(Title = "On Charger disconnected", Icon = Resource.Drawable.power_not_charging)]
        public string OnPowerDisconnected = Intent.ActionPowerDisconnected;
    }
}