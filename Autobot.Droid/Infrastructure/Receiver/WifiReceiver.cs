using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Autobot.Common;
using Autobot.Droid.Infrastructure.Service;
using Autobot.Infrastructure;

namespace Autobot.Droid.Infrastructure.Receiver
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { WifiManager.WifiStateChangedAction, WifiManager.NetworkStateChangedAction })]
    public class WifiReceiver : BroadcastReceiver
    {
        public async override void OnReceive(Context context, Intent intent)
        {
            Main.CurrentContext = context;

            ConnectivityService service = new ConnectivityService();
            string action = intent.Action;
            string state = service.GetWifiState().ToString();

            Rule rule = await Database.Default.GetRuleAsync(intent.Action + Constants.TRIGGER_DELIMITER + state);
            if (rule != null)
            {
                await rule.Run();
            }
        }
    }
}