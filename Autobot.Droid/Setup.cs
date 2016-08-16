using Android.Content;
using Autobot.Droid.Services;
using Autobot.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;

namespace Autobot.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Mvx.RegisterSingleton<IAutobotService>(new AutobotService());
            Mvx.RegisterSingleton<IPresentationService>(new PresentationService());
        }

        protected override IMvxApplication CreateApp()
        {
            return new Autobot.App();
        }
    }
}