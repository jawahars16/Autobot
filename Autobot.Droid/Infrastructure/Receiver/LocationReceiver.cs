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

namespace Autobot.Droid.Infrastructure.Receiver
{
    [BroadcastReceiver]
    [IntentFilter(new string[] {LocationService.GEOFENCE})]
    public class LocationReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string key = intent.GetStringExtra(LocationService.GEOFENCE_KEY);
        }
    }
}