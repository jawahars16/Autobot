using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;

namespace Autobot.Droid.Widgets
{
    public class PixelGridView : View
    {
        private const int OFFSET = 10;
        private const float PIXEL_GRID_SIZE = 50.0f;
        private Paint paint;

        public PixelGridView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            Init();

            int noOfColumns = Width / (int)PIXEL_GRID_SIZE + OFFSET;
            int noOfRows = Height / (int)PIXEL_GRID_SIZE + OFFSET;

            for (int i = 0; i < noOfColumns; i++)
            {
                canvas.DrawLine(i * PIXEL_GRID_SIZE, 0, i * PIXEL_GRID_SIZE, Height, paint);
            }

            for (int i = 0; i < noOfRows; i++)
            {
                canvas.DrawLine(0, i * PIXEL_GRID_SIZE, Width, i * PIXEL_GRID_SIZE, paint);
            }
        }

        private void Init()
        {
            paint = new Paint(PaintFlags.AntiAlias);
            paint.Color = Color.Blue;
            paint.SetStyle(Paint.Style.Stroke);
            paint.SetMaskFilter(new BlurMaskFilter(25, BlurMaskFilter.Blur.Normal));
        }
    }
}