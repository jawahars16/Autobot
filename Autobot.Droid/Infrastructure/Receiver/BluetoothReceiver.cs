using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autobot.Droid.Platform;
using Android.Bluetooth;
using Autobot.Model;
using Autobot.Common;
using System.Threading.Tasks;

namespace Autobot.Droid.Infrastructure.Receiver
{
    [BroadcastReceiver]
    [IntentFilter(new[] {
        BluetoothAdapter.ActionStateChanged,
        BluetoothAdapter.ActionDiscoveryStarted,
        BluetoothAdapter.ActionDiscoveryFinished})]
    public class BluetoothReceiver : BroadcastReceiver
    {
        public async override void OnReceive(Context context, Intent intent)
        {
            Main.CurrentContext = context;
            
            string action = intent.Action;
            string state =  ((State)intent.GetIntExtra(BluetoothAdapter.ExtraState, -1)).ToString();

            List<Rule> rules = await Database.Default.GetRulesAsync(
                intent.Action + Constants.TRIGGER_DELIMITER + state,
                intent.Action);

            if (rules.Any())
            {
                await Task.WhenAll(rules.Select(rule => rule.Run()));
            }
        }
    }
}