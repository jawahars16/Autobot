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
using Autobot.Infrastructure.Triggers;
using Autobot.Attributes;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Trigger(Title = "Time", Icon = Resource.Drawable.time)]
    public class DroidTimeTrigger : TimeTrigger
    {
    }
}