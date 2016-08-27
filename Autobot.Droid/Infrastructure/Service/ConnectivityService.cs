using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;

namespace Autobot.Droid.Infrastructure.Service
{
    public class ConnectivityService
    {
        public WifiState GetWifiState()
        {
            WifiManager manager = Application.Context.GetSystemService(Context.WifiService) as WifiManager;
            return manager.WifiState;
        }
    }
}