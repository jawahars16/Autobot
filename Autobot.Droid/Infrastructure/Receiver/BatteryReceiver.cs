using Android.App;
using Android.Content;
using Autobot.Common;
using Autobot.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autobot.Droid.Infrastructure.Receiver
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { Intent.ActionBatteryLow, Intent.ActionBatteryOkay, Intent.ActionPowerConnected, Intent.ActionPowerDisconnected })]
    public class BatteryReceiver : BroadcastReceiver
    {
        public async override void OnReceive(Context context, Intent intent)
        {
            List<Rule> rules = await Database.Default.GetRulesAsync(intent.Action);
            await Task.WhenAll(rules.Select(rule => rule.Run()));
        }
    }
}