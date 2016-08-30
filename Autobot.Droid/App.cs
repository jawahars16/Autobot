using Android.App;
using Android.Runtime;
using Autobot.Common;
using Autobot.Droid.Platform;
using Autobot.Droid.Services;
using Autobot.Platform;
using Autobot.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System;
using System.IO;

namespace Autobot.Droid
{
    [Application(Theme = "@style/Theme.Autobot")]
    public class App : Application
    {
        public App(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public async override void OnCreate()
        {
            base.OnCreate();

            Container.Default.Register<IReflection>(typeof(Reflection));
            Container.Default.Register<ISchedulerService>(typeof(SchedulerService));

            var filename = "Autobot.sqlite";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);

            await Database.Default.InitializeAsync(path);
        }

        public static AutobotActivity CurrentActivity
        {
            get
            {
                return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as AutobotActivity;
            }
        }
    }
}