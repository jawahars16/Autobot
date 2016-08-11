using Android.App;
using Android.OS;
using Android.Views;
using Autobot.Common;
using Autobot.Droid.Platform;
using Autobot.Platform;
using Com.Lilarcor.Cheeseknife;
using System;

namespace Autobot.Droid.Activities
{
    [Activity(Label = "Rules", MainLauncher = true)]
    public class RulesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RulesActivity);
            Cheeseknife.Inject(this);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Container.Default.Register<IReflection>(typeof(Reflection));
        }

        [InjectOnClick(Resource.Id.newRule)]
        private void OnNewRuleClick(object sender, EventArgs e)
        {
            StartActivity(typeof(NewRuleActivity));
        }
    }
}