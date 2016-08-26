using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Com.Lilarcor.Cheeseknife;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace Autobot.Droid.Views
{
    [Activity(Label = "Rules", MainLauncher = true)]
    public class HomeView : MvxAppCompatActivity
    {
        [InjectView(Resource.Id.toolbar)]
        private Toolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomeActivity);
            Cheeseknife.Inject(this);
            SetSupportActionBar(toolbar);
        }
    }
}