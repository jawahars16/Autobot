using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
        public static int NEW_RULE_REQUEST_CODE = 1000;

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RulesActivity);
            Cheeseknife.Inject(this);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Container.Default.Register<IReflection>(typeof(Reflection));
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        [InjectOnClick(Resource.Id.newRule)]
        private void OnNewRuleClick(object sender, EventArgs e)
        {
            StartActivityForResult(typeof(NewRuleActivity), NEW_RULE_REQUEST_CODE);
        }
    }
}