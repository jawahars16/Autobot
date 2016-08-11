using Android.App;
using Android.Runtime;
using Autobot.Common;
using Autobot.Droid.Platform;
using Autobot.Platform;
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

            var filename = "Autobot.sqlite";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);

            await Database.Default.InitializeAsync(path);
        }
    }
}