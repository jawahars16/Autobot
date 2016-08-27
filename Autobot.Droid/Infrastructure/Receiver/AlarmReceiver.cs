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
    [BroadcastReceiver(Enabled = true, Process = ":remote")]
    public class AlarmReceiver : WakefulBroadcastReceiver
    {
        public async override void OnReceive(Context context, Intent intent)
        {
            string id = intent.GetStringExtra(SchedulerService.RULE_ID);
            double milliSeconds = intent.GetDoubleExtra(SchedulerService.SCHEDULE_TIME, 0);

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
                    service.Schedule(rule);
                }
            }

            CompleteWakefulIntent(intent);
        }
    }
}