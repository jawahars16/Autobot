using Android.App;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Views;

namespace Autobot.Droid.Views
{
    [Activity]
    public class RuleDetailView : MvxActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RuleDetailActivity);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
        }
    }
}