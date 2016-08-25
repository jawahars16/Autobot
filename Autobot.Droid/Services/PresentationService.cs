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
using BetterPickers.RecurrencePickers;
using Android.OS;

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
                
                if(time == Time.Custom)
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

        public Task<Recurrence> PromptRecurrence(string rule = "FREQ=DAILY;WKST=SU")
        {
            var taskCompletionSource = new TaskCompletionSource<Recurrence>();

            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as FragmentActivity;
            if (activity != null)
            {
                RecurrencePickerDialog dialog = new RecurrencePickerDialog();
                Bundle bundle = new Bundle();
                bundle.PutString(RecurrencePickerDialog.BundleRrule, rule);
                dialog.Arguments = bundle;
                dialog.RecurrenceSet += (s, e) =>
                {
                    Recurrence recurrence = new Recurrence(e.Rrule);
                    taskCompletionSource.SetResult(recurrence);
                };
                dialog.Show(activity.SupportFragmentManager, "");
            }

            return taskCompletionSource.Task;
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