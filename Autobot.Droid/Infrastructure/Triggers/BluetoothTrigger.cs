using Android.Bluetooth;
using Autobot.Attributes;
using Autobot.Common;

namespace Autobot.Droid.Infrastructure.Triggers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Trigger(Title = "Bluetooth", Icon = Resource.Drawable.bluetooth)]
    public class BluetoothTrigger
    {
        [Trigger(Title = "On Bluetooth ON", Icon = Resource.Drawable.bluetooth_on)]
        public string OnBluetoothON = BluetoothAdapter.ActionStateChanged + Constants.TRIGGER_DELIMITER + Android.Bluetooth.State.On;

        [Trigger(Title = "On Bluetooth OFF")]
        public string OnBluetoothOFF = BluetoothAdapter.ActionStateChanged + Constants.TRIGGER_DELIMITER + Android.Bluetooth.State.Off;

        [Trigger(Title = "On Discovery started", Icon = Resource.Drawable.discovery)]
        public string OnDiscoveryStarted = BluetoothAdapter.ActionDiscoveryStarted;

        [Trigger(Title = "On Discovery finished")]
        public string OnDiscoveryFinished = BluetoothAdapter.ActionDiscoveryFinished;
    }
}