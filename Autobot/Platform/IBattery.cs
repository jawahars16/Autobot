namespace Autobot.Platform
{
    public enum BatteryState
    {
        Charging,
        Discharging,
        Full,
        NotCharging,
        Unknown
    }

    public enum PowerSource
    {
        Battery,
        Ac,
        Usb,
        Wireless,
        Other
    }

    public interface IBattery
    {
        PowerSource PowerSource { get; }
        int RemainingChargePercent { get; }
        BatteryState State { get; }
    }
}