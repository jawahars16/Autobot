using Android.Content;
using Android.Util;
using Android.Views;
using MvvmCross.Binding.Droid.Views;
using System.Collections.Specialized;

namespace Autobot.Droid.Widgets
{
    public class FlatListView : MvxListView
    {
        private const int ITEM_HEIGHT = 280;
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

        public void Expand(int count)
        {
            ViewGroup.LayoutParams _params = LayoutParameters;
            _params.Height = count * ITEM_HEIGHT;
            LayoutParameters = _params;
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