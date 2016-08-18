using Autobot.Attributes;
using Autobot.Droid.Infrastructure.Service;

namespace Autobot.Droid.Infrastructure.Conditions
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Condition(Title = "Wifi")]
    public class WifiCondition
    {
        [Condition(Title = "If Wifi Disabled", Icon = Resource.Drawable.wifi_disabled)]
        public bool IsWifiOff()
        {
            ConnectivityService service = new ConnectivityService();
            return service.GetWifiState() != Android.Net.WifiState.Enabled;
        }

        [Condition(Title = "If Wifi Enabled", Icon = Resource.Drawable.wifi_enabled)]
        public bool IsWifiOn()
        {
            ConnectivityService service = new ConnectivityService();
            return service.GetWifiState() == Android.Net.WifiState.Enabled;
        }
    }
}