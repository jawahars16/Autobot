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
using Android.Gms.Location;
using Autobot.Common;
using System.Threading.Tasks;

namespace Autobot.Droid.Infrastructure.Receiver
{
    [BroadcastReceiver(Exported = true)]
    [IntentFilter(new string[] {LocationService.GEOFENCE})]
    public class LocationReceiver : BroadcastReceiver
    {
        public async override void OnReceive(Context context, Intent intent)
        {
            var ids = intent.GetStringArrayListExtra(LocationService.GEOFENCE_RULES);
            GeofencingEvent geofencingEvent = GeofencingEvent.FromIntent(intent);

            if (geofencingEvent.HasError)
            {
                int erroCode = geofencingEvent.ErrorCode;
                return;
            }

            // Get the transition type.
            int geofenceTransition = geofencingEvent.GeofenceTransition;

            var rules = await Database.Default.GetRulesAsync(ids.ToArray());
            if (geofenceTransition == Geofence.GeofenceTransitionEnter)
            {
                var validRules = rules.Where(rule => rule.Tag.Contains(LocationService.ARRIVING));
                await Task.WhenAll(validRules.Select(rule => rule.Run()));
            }
        }
    }
}