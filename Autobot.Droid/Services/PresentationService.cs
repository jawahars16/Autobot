using Autobot.Droid.Platform;
using Autobot.Platform;
using Autobot.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autobot.Common;
using System;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Core.ViewModels;
using Autobot.Model;
using Autobot.Droid.Infrastructure.Triggers;
using Android.Widget;

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

        public bool IsTimeTrigger(Trigger trigger)
        {
            return trigger.Type == typeof(TimeTrigger);
        }

        public void PromptTime()
        {

        }

        public void ShowDialog<T>()
        {
            string viewModelName = typeof(T).AssemblyQualifiedName;
            string viewName = typeof(T).FullName.RemoveFromEnd("Model").Replace("Autobot.ViewModel", "Autobot.Droid.Views");
            
            MvxViewModel viewModel = (MvxViewModel)Activator.CreateInstance(Type.GetType(viewModelName));
            MvxDialogFragment dialogFragment = (MvxDialogFragment)Activator.CreateInstance(Type.GetType(viewName));
            dialogFragment.ViewModel = viewModel;

            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as MvxFragmentActivity;
            dialogFragment.Show(activity.SupportFragmentManager, "");
        }
    }
}