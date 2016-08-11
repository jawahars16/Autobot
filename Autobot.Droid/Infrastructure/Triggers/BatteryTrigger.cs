using Android.Content;
using Autobot.Attributes;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Trigger(Title = "Battery")]
    public class BatteryTrigger
    {
        [Trigger(Title = "On Battery Low")]
        public string OnBatteryLow = Intent.ActionBatteryLow;

        [Trigger(Title = "On Battery Okay")]
        public string OnBatteryOkay = Intent.ActionBatteryOkay;

        [Trigger(Title = "On Charger connected")]
        public string OnPowerConnected = Intent.ActionPowerConnected;

        [Trigger(Title = "On Charger disconnected")]
        public string OnPowerDisconnected = Intent.ActionPowerDisconnected;
    }
}