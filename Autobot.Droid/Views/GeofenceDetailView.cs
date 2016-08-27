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

namespace Autobot.Droid.Views
{
    public class GeofenceDetailView : MvxDialogFragment<GeofenceDetailViewModel>
    {
        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    var view = inflater.Inflate(Resource.Layout.geofence_detail_fragment, container, false);
        //    try {
        //        MapsInitializer.Initialize(Activity);
        //    }
        //    catch(Exception e)
        //    {
        //        e.PrintStackTrace();
        //    }

        //    int availability = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(Activity);
            
        //    return view;
        //}
    }
}