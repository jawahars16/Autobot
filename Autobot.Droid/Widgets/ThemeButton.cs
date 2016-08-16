using Android.Content;
using Android.Util;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;

namespace Autobot.Droid.Widgets
{
    public class ThemeButton : FrameLayout
    {
        private const string NAMESPACE = "http://schemas.android.com/apk/res/android";

        [InjectView(Resource.Id.textView)]
        private TextView textView;

        public ThemeButton(Context context, IAttributeSet attr) : base(context, attr)
        {
            Inflate(context, Resource.Layout.ThemeButton, this);
            Cheeseknife.Inject(this, this);
            string text = attr.GetAttributeValue(NAMESPACE, "text");
            textView.Text = text;
        }
    }
}