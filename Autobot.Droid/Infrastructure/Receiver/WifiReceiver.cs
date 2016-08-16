using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Autobot.Common;
using Autobot.Droid.Infrastructure.Service;
using Autobot.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            List<Rule> rules = await Database.Default.GetRulesAsync(intent.Action + Constants.TRIGGER_DELIMITER + state);
            await Task.WhenAll(rules.Select(rule => rule.Run()));
        }
    }
}