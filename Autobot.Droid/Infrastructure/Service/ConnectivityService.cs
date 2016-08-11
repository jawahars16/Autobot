using Android.Content;
using Android.Net;
using Android.Net.Wifi;

namespace Autobot.Droid.Infrastructure.Service
{
    public class ConnectivityService
    {
        public WifiState GetWifiState()
        {
            WifiManager manager = Main.CurrentContext.GetSystemService(Context.WifiService) as WifiManager;
            return manager.WifiState;
        }
    }
}