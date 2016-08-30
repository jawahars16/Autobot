using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autobot.Services;

namespace Autobot.Droid.Services
{
    public class NotificationService : INotificationService
    {
        public void Show(string title, string message)
        {
            Notification.Builder builder = new Notification.Builder(Application.Context)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.Icon);

            // Build the notification:
            Notification notification = builder.Build();
            notification.Priority = (int)Notification.PriorityHigh;

            // Get the notification manager:
            NotificationManager notificationManager = Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}