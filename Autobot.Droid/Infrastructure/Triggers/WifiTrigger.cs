using Android.Net;
using Android.Net.Wifi;
using Autobot.Attributes;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Trigger(Title = "Wifi")]
    public class WifiTrigger
    {
        public const string DELIMITER = "_";

        [Trigger(Title = "On Wifi Disabled")]
        public string OnWifiDisabled = WifiManager.WifiStateChangedAction + DELIMITER + WifiState.Disabled;

        [Trigger(Title = "On Wifi Enabled")]
        public string OnWifiEnabled = WifiManager.WifiStateChangedAction + DELIMITER + WifiState.Enabled;
    }
}