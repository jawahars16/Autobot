using Android.Net;
using Android.Net.Wifi;
using Autobot.Attributes;
using Autobot.Common;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Trigger(Title = "Wifi")]
    public class WifiTrigger
    {
        [Trigger(Title = "On Wifi Disabled")]
        public string OnWifiDisabled = WifiManager.WifiStateChangedAction + Constants.TRIGGER_DELIMITER + WifiState.Disabled;

        [Trigger(Title = "On Wifi Enabled")]
        public string OnWifiEnabled = WifiManager.WifiStateChangedAction + Constants.TRIGGER_DELIMITER + WifiState.Enabled;
    }
}