using Android.App;
using Android.Content;
using Android.Net.Wifi;

namespace Autobot.Droid.Infrastructure.Receiver
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { WifiManager.WifiStateChangedAction, WifiManager.NetworkStateChangedAction })]
    public class WifiReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
        }
    }
}