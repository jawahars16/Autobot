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
using Android.Gms.Location;
using Android.Gms.Common.Apis;

namespace Autobot.Droid.Services
{
    public class LocationService : ILocationService
    {
        //protected GoogleApiClient client;

        //public async void AddGeofence(string key, double latitude, double longitude, int radius)
        //{
        //    new GeofenceBuilder()
        //             .SetRequestId(key)
        //             .SetCircularRegion(
        //                 latitude,
        //                 longitude,
        //                 radius
        //             )
        //             .SetTransitionTypes(Geofence.GeofenceTransitionEnter |
        //                 Geofence.GeofenceTransitionExit)
        //             .Build();
        //}

        //private PendingIntent GetGeofencePendingIntent()
        //{
        //    var intent = new Intent(this, typeof(GeofenceTransitionsIntentService));
        //    return PendingIntent.GetService(this, 0, intent, PendingIntentFlags.UpdateCurrent);
        //}

        //private GeofencingRequest GetGeofencingRequest()
        //{
        //    var builder = new GeofencingRequest.Builder();
        //    builder.SetInitialTrigger(GeofencingRequest.InitialTriggerEnter);

        //    return builder.Build();
        //}
    }
}