using Autobot.Viewmodel;
using MvvmCross.Core.ViewModels;

namespace Autobot
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            //    CreatableTypes()
            //        .EndingWith("Service")
            //        .AsInterfaces()
            //        .RegisterAsLazySingleton();

            RegisterAppStart<HomeViewModel>();
        }
    }
}