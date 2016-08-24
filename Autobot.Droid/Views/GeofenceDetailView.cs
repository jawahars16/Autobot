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
using MvvmCross.Droid.Support.V4;
using Autobot.ViewModel;
using Android.Gms.Maps;
using Autobot.Common;
using Android.Gms.Common;
using MvvmCross.Droid.Views;
using Com.Lilarcor.Cheeseknife;

namespace Autobot.Droid.Views
{
    [Activity]
    public class GeofenceDetailView : MvxActivity
    {
        [InjectView(Resource.Id.map)]
        private MapView mapView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.geofence_detail_activity);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Cheeseknife.Inject(this);
            
            try
            {
                MapsInitializer.Initialize(this);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }

            int availability = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

            if(availability == ConnectionResult.Success)
            {
                mapView.OnCreate(bundle);
            }
        }
    }
}