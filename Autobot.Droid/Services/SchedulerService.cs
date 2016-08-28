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
        public const string RULE_TAG = "RULE_TAG";
        public const string ALARM_TRIGGER = "ALARM_TRIGGER";

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

        public void Schedule(int code, string tag, long interval)
        {
            Intent intent = new Intent(ALARM_TRIGGER);
            intent.PutExtra(RULE_TAG, tag);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(
                Application.Context,
                code,
                intent,
                PendingIntentFlags.UpdateCurrent);

            AlarmManager.Set(
                AlarmType.ElapsedRealtimeWakeup,
                SystemClock.ElapsedRealtime() + interval,
                pendingIntent);
        }

        public void Cancel(int code, string tag)
        {
            Intent intent = new Intent(ALARM_TRIGGER);
            intent.PutExtra(RULE_TAG, tag);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(
                Application.Context,
                code,
                intent,
                PendingIntentFlags.NoCreate);

            AlarmManager.Cancel(pendingIntent);
        }
    }
}