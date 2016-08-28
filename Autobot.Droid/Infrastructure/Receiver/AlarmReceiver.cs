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
using Autobot.Droid.Services;
using Autobot.Model;
using Autobot.Common;
using System.Threading.Tasks;
using Android.Support.V4.Content;
using Autobot.Services;

namespace Autobot.Droid.Infrastructure.Receiver
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new string[]{ SchedulerService.ALARM_TRIGGER })]
    public class AlarmReceiver : BroadcastReceiver
    {
        public async override void OnReceive(Context context, Intent intent)
        {
            string id = intent.GetStringExtra(SchedulerService.RULE_TAG);

            // Run the rules...
            List<Rule> rules = await Database.Default.GetRulesAsync(id);
            await Task.WhenAll(rules.Where(rule => rule.IsEnabled).Select((rule) =>
            {
                return rule.Run();
            }));

            // Re-schedule the rules...
            ISchedulerService service = Container.Default.Resolve<ISchedulerService>();
            foreach (var rule in rules)
            {
                if (rule.IsEnabled)
                {
                    service.Schedule(rule.Id.GetHashCode(), rule.Tag, rule.Trigger.TriggerInterval);
                }
            }
        }
    }
}