using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Autobot.Droid.Adapters;
using Autobot.Droid.Fragments;
using Autobot.Infrastructure;
using Com.Lilarcor.Cheeseknife;
using System;

namespace Autobot.Droid.Activities
{
    [Activity(Label = "NewRuleActivity")]
    public class NewRuleActivity : FragmentActivity
    {
        private Rule rule;

        [InjectView(Resource.Id.pager)]
        private ViewPager viewPager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewRuleActivity);
            Cheeseknife.Inject(this);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            var adapter = new GenericFragmentPagerAdapter(SupportFragmentManager);
            rule = new Rule();

            var actionPicker = new ActionPickerFragment(rule);

            adapter.AddFragment(new TriggerPickerFragment(rule));
            adapter.AddFragment(new ConditionPickerFragment(rule));
            adapter.AddFragment(actionPicker);

            actionPicker.OnFinish += OnFinishRule;

            viewPager.Adapter = adapter;
        }

        private void OnFinishRule(object sender, EventArgs e)
        {
            Main.CurrentContext = this;
            rule.Run();
        }
    }
}