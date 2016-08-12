using Android.Content;

namespace Autobot.Droid.Infrastructure.Receiver
{
    [BroadcastReceiver]
    public class BatteryReceiver : BroadcastReceiver
    {
        public async override void OnReceive(Context context, Intent intent)
        {
            //List<Rule> rules = await Database.Default.GetRulesAsync(intent.Action);
        }
    }
}