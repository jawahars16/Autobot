using Autobot.Droid.Platform;
using Autobot.Platform;
using Autobot.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autobot.Droid.Services
{
    public class PresentationService : IPresentationService
    {
        public async Task<ISelectable> SelectFromGridAsync(IEnumerable<ISelectable> source)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            return await Prompt.Make(activity, source).ShowAsync(false);
        }

        public async Task<ISelectable> SelectFromListAsync(IEnumerable<ISelectable> source)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            return await Prompt.Make(activity, source).ShowAsync(true);
        }
    }
}