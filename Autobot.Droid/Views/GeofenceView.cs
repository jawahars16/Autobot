using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Support.V4;
using Autobot.ViewModel;
using Com.Lilarcor.Cheeseknife;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V7.Widget;

namespace Autobot.Droid.Views
{
    [Activity(Label = "Geofence")]
    public class GeofenceView : MvxAppCompatActivity
    {
        [InjectView(Resource.Id.toolbar)]
        private Toolbar toolbar;

        public GeofenceViewModel ThisViewModel
        {
            get
            {
                return ViewModel as GeofenceViewModel;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_GeofenceList);
            Cheeseknife.Inject(this);
            toolbar.SetNavigationIcon(Resource.Drawable.menu);
            SetSupportActionBar(toolbar);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                ThisViewModel.MenuCommand.Execute();
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            base.OnResume();
            ThisViewModel.Resume();
        }
    }
}