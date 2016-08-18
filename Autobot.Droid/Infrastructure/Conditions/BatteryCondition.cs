using Autobot.Attributes;
using Autobot.Droid.Platform;

namespace Autobot.Droid.Infrastructure.Conditions
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Condition(Title = "Battery")]
    public class BatteryCondition
    {
        [Condition(Title = "If Charger connected", Icon = Resource.Drawable.power_connected)]
        public bool IsAcCharger()
        {
            Battery battery = new Battery();
            return battery.PowerSource == Autobot.Platform.PowerSource.Ac;
        }

        [Condition(Title = "If USB Charger connected", Icon = Resource.Drawable.power_usb)]
        public bool IsUSBCharger()
        {
            Battery battery = new Battery();
            return battery.PowerSource == Autobot.Platform.PowerSource.Usb;
        }

        [Condition(Title = "If Charging wireless", Icon = Resource.Drawable.power_wireless)]
        public bool IsWirelessCharger()
        {
            Battery battery = new Battery();
            return battery.PowerSource == Autobot.Platform.PowerSource.Wireless;
        }

        [Condition(Title = "If battery full", Icon = Resource.Drawable.battery_full)]
        public bool IsBatteryFull()
        {
            Battery battery = new Battery();
            return battery.State == Autobot.Platform.BatteryState.Full;
        }
        
        [Condition(Title = "If not charging", Icon = Resource.Drawable.power_not_charging)]
        public bool IsBatteryNotCharging()
        {
            Battery battery = new Battery();
            return battery.State == Autobot.Platform.BatteryState.NotCharging;
        }
    }
}