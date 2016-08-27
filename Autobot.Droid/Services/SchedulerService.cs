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
using Autobot.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using Autobot.Droid.Infrastructure.Receiver;
using Autobot.Model;
using Autobot.Common;

namespace Autobot.Droid.Services
{
    public class SchedulerService : ISchedulerService
    {
        public const string SCHEDULE_TIME = "SCHEDULE_TIME";
        public const string RULE_ID = "RULE_ID";

        private AlarmManager manager;

        public AlarmManager AlarmManager
        {
            get
            {
                if(manager == null)
                {
                    manager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
                }
                return manager;
            }
        }

        public void Schedule(Rule rule)
        {
            string timeSpanStr = rule.Trigger.Id.Replace(Constants.TIME_TRIGGER,"");
            TimeSpan time = TimeSpan.Parse(timeSpanStr);

            AlarmManager.Set(
                AlarmType.ElapsedRealtimeWakeup,
                (long)time.TotalMilliseconds, 
                GetPendingIntent(time));
        }

        public void Cancel(Rule rule)
        {
            string timeSpanStr = rule.Trigger.Id.Replace(Constants.TIME_TRIGGER, "");
            TimeSpan time = TimeSpan.Parse(timeSpanStr);

            AlarmManager.Cancel(GetPendingIntent(time));
        }

        private PendingIntent GetPendingIntent(TimeSpan timespan)
        {
            Intent intent = new Intent(Application.Context, typeof(AlarmReceiver));
            intent.PutExtra(RULE_ID, $"{Constants.TIME_TRIGGER}{timespan.ToString()}");
            intent.PutExtra(SCHEDULE_TIME, timespan.TotalMilliseconds);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(
                Application.Context, 
                timespan.ToString().GetHashCode(), 
                intent, 
                PendingIntentFlags.OneShot);
            return pendingIntent;
        }
    }
}