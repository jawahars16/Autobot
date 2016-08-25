using Autobot.Model;
using Autobot.Services;
using MvvmCross.Core.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.ViewModel
{
    [ImplementPropertyChanged]
    public class NewScheduleViewModel : MvxViewModel
    {
        private readonly IPresentationService presentationService;

        public Date Date { get; set; }
        public Time Time { get; set; }
        public Recurrence Recurrence { get; set; }

        public IMvxCommand DoneCommand { get; set; }
        public IMvxCommand DatePickerCommand { get; set; }
        public IMvxCommand TimePickerCommand { get; set; }
        public IMvxCommand RecurrencePickerCommand { get; set; }

        public NewScheduleViewModel(IPresentationService presentationService)
        {
            DoneCommand = new MvxCommand(OnDone);
            TimePickerCommand = new MvxCommand(OnTimePick);
            DatePickerCommand = new MvxCommand(OnDatePick);
            RecurrencePickerCommand = new MvxCommand(OnRecurrencePick);

            Date = presentationService.GetDefaultDate();
            Time = presentationService.GetDefaultTime();

            this.presentationService = presentationService;
        }

        private async void OnRecurrencePick()
        {
            Recurrence = await presentationService.PromptRecurrence(Recurrence?.RecurrenceRule);
        }

        private async void OnDatePick()
        {
            Date = await presentationService.PromptDate();
        }

        private async void OnTimePick()
        {
            Time = await presentationService.PromptTime();
        }

        private void OnDone()
        {
            Close(this);
        }
    }
}
