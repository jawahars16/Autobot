using Android.App;
using Android.Widget;
using Autobot.Attributes;

namespace Autobot.Droid.Infrastructure.Actions
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Action(Title = "Notification")]
    public class NotificationAction
    {
        [Action(Title = "Show push notification", Icon = Resource.Drawable.notification)]
        public void ShowPushNotification()
        {
            Toast.MakeText(Application.Context, "Showing push notification", ToastLength.Long).Show();
        }

        [Action(Title = "Show toast message", Icon = Resource.Drawable.notification)]
        public void ShowToast()
        {
            Toast.MakeText(Application.Context, "Toast message", ToastLength.Long).Show();
        }
    }
}