using System;
using Android.App;
using Android.OS;
using Android.Views;
using Autobot.Droid.Widgets;
using Autobot.ViewModel;
using Com.Lilarcor.Cheeseknife;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Autobot.Droid.Views
{
    [Activity]
    public class RuleDetailView : MvxAppCompatActivity
    {
        private IMenu menu;

        [InjectView(Resource.Id.toolbar)]
        private Toolbar toolbar;

        [InjectView(Resource.Id.triggersListView)]
        private FlatListView triggersListView;

        [InjectView(Resource.Id.actionsListView)]
        private FlatListView actionsListView;

        [InjectView(Resource.Id.conditionsListView)]
        private FlatListView conditionsListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_RuleDetail);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Cheeseknife.Inject(this);
            SetSupportActionBar(toolbar);
            
            ThisViewModel.SaveCommand.CanExecuteChanged += OnCanExecuteChanged;
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            actionsListView.Initialize();
            actionsListView.Expand(actionsListView.Adapter.Count);

            conditionsListView.Initialize();
            conditionsListView.Expand(conditionsListView.Adapter.Count);
        }

        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            menu.GetItem(0).SetEnabled(ThisViewModel.SaveCommand.CanExecute());
        }

        public RuleDetailViewModel ThisViewModel
        {
            get
            {
                return ViewModel as RuleDetailViewModel;
            }
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.menu = menu;
            MenuInflater.Inflate(Resource.Menu.common_action_bar_menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.done:
                    ThisViewModel.SaveCommand.Execute();
                    break;
                case Resource.Id.delete:
                    ThisViewModel.DeleteCommand.Execute();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}