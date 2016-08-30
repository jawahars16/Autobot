using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Autobot.Droid.Platform
{
    public delegate void PermissionResultEventHandler(object sender, PermissionResultEventArgs e);

    public class PermissionResultEventArgs : EventArgs
    {
        public PermissionResultEventArgs(int requestCode, string[] permissions)
        {
            RequestCode = requestCode;
            Permissions = permissions;
        }

        public int RequestCode { get; set; }
        public string[] Permissions { get; set; }
    }

    public class AutobotActivity : MvxAppCompatActivity
    {
        public event PermissionResultEventHandler PermissionResult;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions)
        {
            base.OnRequestPermissionsResult(requestCode, permissions);
            if(PermissionResult != null)
            {
                PermissionResult(this, new PermissionResultEventArgs(requestCode, permissions));
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (PermissionResult != null)
            {
                PermissionResult(this, new PermissionResultEventArgs(requestCode, permissions));
            }
        }
    }
}