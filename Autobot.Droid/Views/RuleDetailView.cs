using System;
using Android.App;
using Android.OS;
using Android.Views;
using Autobot.Droid.Widgets;
using Autobot.ViewModel;
using Com.Lilarcor.Cheeseknife;
using MvvmCross.Droid.Views;

namespace Autobot.Droid.Views
{
    [Activity]
    public class RuleDetailView : MvxActivity
    {
        [InjectView(Resource.Id.actionsListView)]
        private FlatListView actionsListView;

        [InjectView(Resource.Id.conditionsListView)]
        private FlatListView conditionsListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RuleDetailActivity);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Cheeseknife.Inject(this);

            ((RuleDetailViewModel)ViewModel).Initialized += OnInitialized;
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            actionsListView.Initialize();
            actionsListView.Expand(actionsListView.Adapter.Count);

            conditionsListView.Initialize();
            conditionsListView.Expand(conditionsListView.Adapter.Count);
        }
    }
}