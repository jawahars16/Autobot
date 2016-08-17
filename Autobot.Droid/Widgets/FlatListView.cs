using Android.Content;
using Android.Util;
using Android.Views;
using MvvmCross.Binding.Droid.Views;

namespace Autobot.Droid.Widgets
{
    public class FlatListView : MvxListView
    {
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

            if (e.ActionMasked == MotionEventActions.Move)
            {
                return true;
            }

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
    }
}