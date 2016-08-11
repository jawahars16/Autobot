using Android.App;
using Android.Runtime;
using Autobot.Common;
using System;
using System.IO;

namespace Autobot.Droid
{
    [Application]
    public class App : Application
    {
        public App(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var filename = "Autobot.sqlite";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);

            Database.Default.Initialize(path);
        }
    }
}