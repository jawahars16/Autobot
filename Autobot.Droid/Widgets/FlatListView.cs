using Android.Content;
using Android.Util;
using Android.Views;
using MvvmCross.Binding.Droid.Views;
using System.Collections.Specialized;

namespace Autobot.Droid.Widgets
{
    public class FlatListView : MvxListView
    {
        private const int FOOTER_HEIGHT = 80;
        private const int ITEM_HEIGHT = 300;
        private int mPosition;

        public FlatListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            if (e.ActionMasked == MotionEventActions.Down)
            {
                mPosition = PointToPosition((int)e.XPrecision, (int)e.YPrecision);
                return base.DispatchTouchEvent(e);
            }

            if (e.ActionMasked == MotionEventActions.Move) { return true; }

            if (e.ActionMasked == MotionEventActions.Up)
            {
                if (PointToPosition((int)e.XPrecision, (int)e.YPrecision) == mPosition)
                {
                    return base.DispatchTouchEvent(e);
                }
                else
                {
                    DispatchSetPressed(false);
                    Invalidate();
                    return true;
                }
            }
            return base.DispatchTouchEvent(e);
        }

        public void Expand(int count)
        {
            int desiredWidth = MeasureSpec.MakeMeasureSpec(Width, MeasureSpecMode.Unspecified);
            int totalHeight = 0;

            View view = null;
            for (int i = 0; i < Adapter.Count; i++)
            {
                view = Adapter.GetView(i, view, this);
                if (i == 0)
                    view.LayoutParameters = new ViewGroup.LayoutParams(desiredWidth, LayoutParams.WrapContent);

                view.Measure(desiredWidth, (int)MeasureSpecMode.Unspecified);
                totalHeight += view.MeasuredHeight;
            }

            LayoutParameters.Height = totalHeight + (DividerHeight * (Adapter.Count - 1));
        }

        public void Initialize()
        {
            var source = Adapter.ItemsSource as INotifyCollectionChanged;
            if (source != null)
            {
                source.CollectionChanged += OnCollectionChanged;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                var source = Adapter.ItemsSource as INotifyCollectionChanged;
                if (source != null)
                {
                    source.CollectionChanged -= OnCollectionChanged;
                }
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Expand(ChildCount + 1);
            }
        }
    }
}