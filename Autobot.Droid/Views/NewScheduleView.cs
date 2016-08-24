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
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Support.V4;
using Com.Lilarcor.Cheeseknife;
using Android.Support.V7.App;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Autobot.Droid.Views
{
    [Activity(Label = "New Schedule", ParentActivity = typeof(HomeView))]
    public class NewScheduleView : MvxAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_NewSchedule);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            
        }
    }
}