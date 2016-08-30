using Android.App;
using Android.Widget;
using Autobot.Attributes;
using Autobot.Services;
using MvvmCross.Platform;

namespace Autobot.Droid.Infrastructure.Actions
{
    [Action(Title = "Notification")]
    public class NotificationAction
    {
        [Action(Title = "Show push notification", Icon = Resource.Drawable.notification)]
        public void ShowPushNotification()
        {
            var service = Mvx.Resolve<INotificationService>();
            service.Show("Autobot", "Showing push notification");
        }

        [Action(Title = "Show toast message", Icon = Resource.Drawable.notification)]
        public void ShowToast()
        {
            Toast.MakeText(Application.Context, "Toast message", ToastLength.Long).Show();
        }
    }
}