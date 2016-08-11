using Autobot.Attributes;
using Autobot.Droid.Infrastructure.Service;

namespace Autobot.Droid.Infrastructure.Conditions
{
    [Condition(Title = "Wifi")]
    public class WifiCondition
    {
        [Condition(Title = "If Wifi Disabled")]
        public bool IsWifiOff()
        {
            ConnectivityService service = new ConnectivityService();
            return service.GetWifiState() != Android.Net.WifiState.Enabled;
        }

        [Condition(Title = "If Wifi Enabled")]
        public bool IsWifiOn()
        {
            ConnectivityService service = new ConnectivityService();
            return service.GetWifiState() == Android.Net.WifiState.Enabled;
        }
    }
}