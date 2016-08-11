using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autobot.Droid.Presenter;
using Com.Lilarcor.Cheeseknife;
using System;

namespace Autobot.Droid.Activities
{
    [Activity(Label = "Rules", MainLauncher = true)]
    public class RulesActivity : Activity
    {
        public static int NEW_RULE_REQUEST_CODE = 1000;
        private RulesPresenter presenter;

        [InjectView(Resource.Id.rulesList)]
        private ListView rulesList;

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
            presenter = new RulesPresenter();
        }

        protected async override void OnResume()
        {
            base.OnResume();
            var rules = await presenter.GetRules();
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, rules);
            rulesList.Adapter = adapter;
        }

        [InjectOnClick(Resource.Id.newRule)]
        private void OnNewRuleClick(object sender, EventArgs e)
        {
            StartActivityForResult(typeof(NewRuleActivity), NEW_RULE_REQUEST_CODE);
        }
    }
}