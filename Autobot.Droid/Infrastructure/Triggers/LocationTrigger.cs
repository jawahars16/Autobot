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
using Autobot.Attributes;
using Autobot.Droid.Services;
using Autobot.Common;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Trigger(Title = "Location", Icon = Resource.Drawable.location)]
    public class LocationTrigger
    {
        [Trigger(Title = "On arriving...")]
        public string OnArriving = $"{LocationService.GEOFENCE}{Constants.TRIGGER_DELIMITER}{LocationService.ARRIVING}";

        [Trigger(Title = "On leaving...")]
        public string OnManual = $"{LocationService.GEOFENCE}{Constants.TRIGGER_DELIMITER}{LocationService.LEAVING}";

        public static LocationTrigger Default { get; private set; } = new LocationTrigger();
    }
}