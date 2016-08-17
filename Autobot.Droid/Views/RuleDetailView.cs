using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using MvvmCross.Droid.Views;

namespace Autobot.Droid.Views
{
    [Activity]
    public class RuleDetailView : MvxActivity, View.IOnTouchListener
    {
        [InjectView(Resource.Id.actionsListView)]
        private ListView actionsListView;

        [InjectView(Resource.Id.conditionsListView)]
        private ListView conditionsListView;

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
                totalHeight += view.MeasuredHeight;
            }
            ViewGroup.LayoutParams _params = listView.LayoutParameters;
            _params.Height = totalHeight + (listView.DividerHeight * (listAdapter.Count - 1));
            listView.LayoutParameters = _params;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            v.Parent.RequestDisallowInterceptTouchEvent(true);
            return false;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RuleDetailActivity);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Cheeseknife.Inject(this);

            actionsListView.SetOnTouchListener(this);
            conditionsListView.SetOnTouchListener(this);

            ExpandListView(conditionsListView);
            ExpandListView(actionsListView);
        }
    }
}