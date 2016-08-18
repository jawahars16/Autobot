using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Autobot.Model;
using Autobot.ViewModel;
using Com.Lilarcor.Cheeseknife;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Views;
using System.Collections.Specialized;

namespace Autobot.Droid.Views
{
    [Activity]
    public class RuleDetailView : MvxActivity
    {
        [InjectView(Resource.Id.actionsView)]
        private LinearLayout actionsView;

        [InjectView(Resource.Id.conditionsListView)]
        private ListView conditionsListView;

        private int itemHeight = 0;
        private Rule rule;

        public void ExpandListView(ListView listView)
        {
            var listAdapter = conditionsListView.Adapter;
            if (listAdapter == null)
                return;

            int desiredWidth = View.MeasureSpec.MakeMeasureSpec(listView.Width, MeasureSpecMode.Unspecified);
            int totalHeight = 0;
            View view = null;
            for (int i = 0; i < listAdapter.Count; i++)
            {
                view = listAdapter.GetView(i, view, listView);
                if (i == 0)
                    view.LayoutParameters = new ViewGroup.LayoutParams(desiredWidth, ActionBar.LayoutParams.WrapContent);

                view.Measure(desiredWidth, (int)MeasureSpecMode.Unspecified);
                itemHeight = view.MeasuredHeight;
                totalHeight += (view.MeasuredHeight + itemHeight);
            }
            ViewGroup.LayoutParams _params = listView.LayoutParameters;
            _params.Height = totalHeight + (listView.DividerHeight * (listAdapter.Count - 1));
            listView.LayoutParameters = _params;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RuleDetailActivity);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Cheeseknife.Inject(this);

            rule = ((RuleDetailViewModel)ViewModel)?.Rule;
            rule.Actions.CollectionChanged += OnActionsCollectionChanged;
            rule.Conditions.CollectionChanged += OnConditionsCollectionChanged;
        }

        private void OnActionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var view = LayoutInflater.Inflate(Resource.Layout.action_item, null);

            var binding = new MvxAndroidBindingContext(this, this);
            binding.DataContext = rule;
            actionsView.AddView(view);
        }

        private void OnChildViewAdded(object sender, ViewGroup.ChildViewAddedEventArgs e)
        {
            ListView listView = sender as ListView;
            ExpandListView(listView);
        }

        private void OnConditionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
    }
}