using Android.Widget;
using Autobot.Attributes;

namespace Autobot.Droid.Infrastructure.Actions
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Action(Title = "Notification")]
    public class NotificationAction
    {
        [Action(Title = "Show push notification")]
        public void ShowPushNotification()
        {
            Toast.MakeText(Main.CurrentContext, "Showing push notification", ToastLength.Long).Show();
        }

        [Action(Title = "Show toast message")]
        public void ShowToast()
        {
            Toast.MakeText(Main.CurrentContext, "Toast message", ToastLength.Long).Show();
        }
    }
}