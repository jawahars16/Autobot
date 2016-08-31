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
using MvvmCross.Platform;
using Autobot.Services;

namespace Autobot.Droid.Infrastructure.Receiver
{
    [BroadcastReceiver(Exported = true)]
    [IntentFilter(new string[] { App.RULE_STARTED, App.RULE_FAILED, App.RULE_SUCCESS })]
    public class AutobotReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            string message = intent.GetStringExtra(App.RULE_REPORT_MESSAGE);

            if (action == App.RULE_FAILED)
            {
                var notificationService = Mvx.Resolve<INotificationService>();
                notificationService.Show("Autobot", message);
            }
            else if (action == App.RULE_STARTED)
            {
                var notificationService = Mvx.Resolve<INotificationService>();
                notificationService.Show("Autobot", message);
            }
            else if (action == App.RULE_SUCCESS)
            {
                var notificationService = Mvx.Resolve<INotificationService>();
                notificationService.Show("Autobot", message);
            }
        }
    }
}