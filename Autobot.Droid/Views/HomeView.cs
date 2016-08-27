using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Autobot.Viewmodel;
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

        public HomeViewModel ThisViewModel
        {
            get
            {
                return ViewModel as HomeViewModel;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_Home);
            Cheeseknife.Inject(this);
            SetSupportActionBar(toolbar);
        }

        protected override void OnResume()
        {
            base.OnResume();
            ThisViewModel.Resume();
        }
    }
}