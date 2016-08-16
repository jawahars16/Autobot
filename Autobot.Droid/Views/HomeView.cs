using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Autobot.Droid.Views
{
    [Activity(Label = "Rules", MainLauncher = true)]
    public class HomeView : MvxActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomeActivity);
        }
    }
}