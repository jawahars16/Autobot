using Autobot.Attributes;
using Autobot.Droid.Platform;

namespace Autobot.Droid.Infrastructure.Conditions
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Condition(Title = "Battery")]
    public class BatteryCondition
    {
        [Condition(Title = "If Charger connected")]
        public bool IsAcCharger()
        {
            Battery battery = new Battery();
            return battery.PowerSource == Autobot.Platform.PowerSource.Ac;
        }
    }
}