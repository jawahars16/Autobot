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
using Android.Net.Wifi;
using Android.Net;
using System.Reflection;

namespace Autobot.Droid.Platform
{
    public enum HotspotState
    {
        Enabled, 

        Disabled
    }

    public class NetworkManager
    {
        private WifiManager wifiManager;

        public NetworkManager(Context context)
        {
            wifiManager = context.GetSystemService(Context.WifiService) as WifiManager;
        }

        public WifiState GetWifiState()
        {
            return wifiManager.WifiState;
        }

        public HotspotState GetHotspotState()
        {
            var method = wifiManager.GetType().GetMethod("isWifiApEnabled");
            var enabled = (bool)method.Invoke(wifiManager, null);
            return enabled ? HotspotState.Enabled : HotspotState.Disabled;
        }

        public WifiConfiguration ConfigureHotspot(string username, string password)
        {
            var method = wifiManager.GetType().GetMethod("getWifiApConfiguration");
            WifiConfiguration configuration = (WifiConfiguration)method.Invoke(wifiManager, null);
            configuration.Ssid = username;
            configuration.PreSharedKey = password;
            return configuration;
        }

        public void SetHotspotState(WifiConfiguration configuration, bool enabled)
        {
            var method = wifiManager.GetType().GetMethod("setWifiApEnabled");
            method.Invoke(wifiManager, new object[] { configuration, enabled });
        }
    }
}