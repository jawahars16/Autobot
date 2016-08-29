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
using Android.Support.V4.App;
using Android.App;
using Android.OS;
using System.Linq;
using Autobot.Viewmodel;
using Autobot.ViewModel;

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

        public Date GetDefaultDate()
        {
            return Date.Today;
        }

        public Time GetDefaultTime()
        {
            DateTime now = DateTime.Now;

            if (now.TimeOfDay < TimeSpan.FromHours(8))
            {
                return Time.Morning;
            }
            else if (now.TimeOfDay < TimeSpan.FromHours(13))
            {
                return Time.Noon;
            }
            else if (now.TimeOfDay < TimeSpan.FromHours(18))
            {
                return Time.Evening;
            }
            else
            {
                return Time.Custom;
            }
        }

        public async Task<Date> PromptDate()
        {
            var taskCompletionSource = new TaskCompletionSource<Date>();

            IEnumerable<ISelectable> options = new List<ISelectable>
            {
               Date.Today,
               Date.Tomorrow,
               Date.NextDay,
               Date.Custom
            };

            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            if (activity != null)
            {
                ISelectable option = await Prompt.Make(activity, options).ShowAsync();
                Date date = option as Date;

                if (date == Date.Custom)
                {
                    DateTime now = DateTime.Now;
                    DatePickerDialog dialog = new DatePickerDialog(activity, (s, e) =>
                    {
                        taskCompletionSource.SetResult(Date.Create(e.Date, e.Date.ToReadableFormat()));
                    }, now.Year, now.Month, now.Day);
                    dialog.Show();
                }
                else
                {
                    taskCompletionSource.SetResult(date);
                }

            }

            return await taskCompletionSource.Task;
        }

        public async Task<Time> PromptTime()
        {
            var taskCompletionSource = new TaskCompletionSource<Time>();

            IEnumerable<ISelectable> options = new List<ISelectable>
            {
               Time.Morning,
               Time.Noon,
               Time.Evening,
               Time.Night,
               Time.Custom
            };

            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            if (activity != null)
            {
                ISelectable option = await Prompt.Make(activity, options).ShowAsync();
                Time time = option as Time;

                if (time == Time.Custom)
                {
                    DateTime now = DateTime.Now;
                    TimePickerDialog dialog = new TimePickerDialog(activity, (s, e) =>
                    {
                        TimeSpan _time = new TimeSpan(e.HourOfDay, e.Minute, 0);
                        taskCompletionSource.SetResult(Time.Create(_time, _time.ToReadableFormat()));
                    }, now.Hour, now.Minute, false);
                    dialog.Show();
                }
                else
                {
                    taskCompletionSource.SetResult(time);
                }

            }

            return await taskCompletionSource.Task;
        }

        public async Task<IEnumerable<WeekDay>> PromptWeekDays()
        {
            IEnumerable<ISelectable> options = new List<ISelectable>
            {
               WeekDay.Sunday,
               WeekDay.Monday,
               WeekDay.Tuesday,
               WeekDay.Wednesday,
               WeekDay.Thursday,
               WeekDay.Friday,
               WeekDay.Saturday
            };

            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            if (activity != null)
            {
                List<ISelectable> selectedOptions = await Prompt.Make(activity, options).ShowMultipleAsync();
                return selectedOptions.Cast<WeekDay>();
            }

            return null;
        }

        public async Task<NavigationItem> RequestNavigation()
        {
            IEnumerable<NavigationItem> options = new List<NavigationItem>
            {
                new NavigationItem("My Rules", -1, typeof(HomeViewModel)),
                new NavigationItem("Geofence", -1, typeof(GeofenceViewModel))
            };


            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            if (activity != null)
            {
                NavigationItem item = (NavigationItem)await Prompt.Make(activity, options).ShowAsync();
                return item;
            }

            return null;
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

        public async Task<string> PromptText(string title, string description)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            return await Prompt.Make(activity).ShowAsync(title, description);
        }
    }
}