using Android.Net;
using Android.Net.Wifi;
using Autobot.Attributes;
using Autobot.Common;
using Autobot.Droid.Platform;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Trigger(Title = "Wifi", Icon = Resource.Drawable.wifi)]
    public class WifiTrigger
    {
        [Trigger(Title = "On Wifi Disabled")]
        public string OnWifiDisabled = WifiManager.WifiStateChangedAction + Constants.TRIGGER_DELIMITER + WifiState.Disabled;

        [Trigger(Title = "On Wifi Enabled")]
        public string OnWifiEnabled = WifiManager.WifiStateChangedAction + Constants.TRIGGER_DELIMITER + WifiState.Enabled;

        [Trigger(Title = "On Wifi Hotspot Enabled")]
        public string OnWifiHotspotEnabled = WifiManager.WifiStateChangedAction + Constants.TRIGGER_DELIMITER + HotspotState.Enabled;

        [Trigger(Title = "On Wifi Hotspot Disabled")]
        public string OnWifiHotspotDisabled = WifiManager.WifiStateChangedAction + Constants.TRIGGER_DELIMITER + HotspotState.Disabled;
    }
}