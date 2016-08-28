using Autobot.Common;
using Autobot.Infrastructure.Triggers;
using Autobot.Model;
using Autobot.Platform;
using Autobot.Services;
using MvvmCross.Core.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autobot.ViewModel
{
    [ImplementPropertyChanged]
    public class RuleDetailViewModel : MvxViewModel
    {
        private readonly IAutobotService autobotService;
        private readonly IPresentationService presentationService;
        private readonly ISchedulerService schedulerService;

        public RuleDetailViewModel(
            IAutobotService autobotService, 
            IPresentationService presentationService,
            ISchedulerService schedulerService)
        {
            this.autobotService = autobotService;
            this.presentationService = presentationService;
            this.schedulerService = schedulerService;
            SetTriggerCommand = new MvxCommand(OnSetTrigger);
            AddConditionCommand = new MvxCommand(OnAddCondition);
            AddActionCommand = new MvxCommand(OnAddAction);
            SaveCommand = new MvxCommand(OnSaveRule, CanSaveRule);
            DeleteCommand = new MvxCommand(OnDelete);
        }

        private async void OnDelete()
        {
            await Rule.Delete();
            if (Rule.Trigger.IsTimeTrigger)
            {
                schedulerService.Cancel(Rule.Id.GetHashCode(), Rule.Tag);
            }
            Close(this);
        }

        private bool CanSaveRule()
        {
            return Rule.Trigger != null && Rule.Actions.Any();
        }

        public event EventHandler Initialized;

        public IMvxCommand AddActionCommand { get; set; }
        public IMvxCommand AddConditionCommand { get; set; }
        public bool IsInEditMode { get; set; }
        public Rule Rule { get; set; }
        public IMvxCommand SaveCommand { get; set; }
        public IMvxCommand DeleteCommand { get; set; }
        public IMvxCommand SetTriggerCommand { get; set; }
        public bool ShowConditionsArrow { get; set; }

        public async Task HandleTimeTrigger(Trigger trigger)
        {
            Time time = null;
            if (TimeTrigger.Default.EveryDay == trigger.Tag)
            {
                time = await presentationService.PromptTime();
                trigger.Title = $"Every {time.Title}";
            }
            else if (TimeTrigger.Default.EveryWeek == trigger.Tag)
            {
                IEnumerable<WeekDay> days = await presentationService.PromptWeekDays();
                time = await presentationService.PromptTime();

                if (days.Count() == 5 && !days.Contains(WeekDay.Saturday) && !days.Contains(WeekDay.Sunday))
                {
                    // This is week days.
                    trigger.Title = $"Every weekdays {time.Title}";
                }
                else if (days.Count() == 2 && days.Contains(WeekDay.Saturday) && days.Contains(WeekDay.Sunday))
                {
                    // This is week end.
                    trigger.Title = $"Every weekend {time.Title}";
                }
                else if (days.Count() == 7)
                {
                    trigger.Title = $"Every {time.Title}";
                }
                else
                {
                    trigger.Title = "Every ";
                    trigger.Title += " " + string.Join(", ", days.Select(
                        day => day.Title));
                    int lastCommaIndex = trigger.Title.LastIndexOf(", ");
                    if (lastCommaIndex > 0)
                    {
                        trigger.Title = trigger.Title.Remove(lastCommaIndex, 2);
                        trigger.Title = trigger.Title.Insert(lastCommaIndex, " and ");
                        trigger.Title += $" {time.Title}";
                    }
                }
            }
            else if (TimeTrigger.Default.Custom == trigger.Tag)
            {
                IEnumerable<CustomTime> timeList = new List<CustomTime>
                {
                    CustomTime.EVERY_5_SECONDS,
                    CustomTime.EVERY_30_MINUTES,
                    CustomTime.EVERY_4_HOURS,
                    CustomTime.EVERY_8_HOURS,
                    CustomTime.EVERY_2_DAYS,
                    CustomTime.EVERY_10_DAYS,
                    CustomTime.EVERY_20_DAYS
                };

                time = (CustomTime)await presentationService.SelectFromListAsync(timeList);
                trigger.Title = time.Title;
            }

            if (time != null)
            {
                trigger.TriggerTime = time.Value;
            }
        }

        protected async override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            if (parameters.Data.ContainsKey("Id"))
            {
                string id = parameters.Data["Id"];
                var rule = await Database.Default.GetRuleAsync(id);
                await rule.Load();
                Rule = rule;
                IsInEditMode = false;
                ShowConditionsArrow = Rule.Conditions.Any();
            }
            else
            {
                Rule = new Rule();
                IsInEditMode = true;
                ShowConditionsArrow = true;
            }

            Initialized?.Invoke(this, null);
        }

        private async void OnAddAction()
        {
            var actions = autobotService.GetActions();
            var action = await presentationService.SelectFromGridAsync(actions);

            actions = autobotService.GetActions((Model.Action)action);
            action = await presentationService.SelectFromListAsync(actions);

            Rule.Actions.Add((Model.Action)action);
        }

        private async void OnAddCondition()
        {
            var conditions = autobotService.GetConditions();
            var condition = await presentationService.SelectFromGridAsync(conditions);

            conditions = autobotService.GetConditions((Condition)condition);
            condition = await presentationService.SelectFromListAsync(conditions);

            Rule.Conditions.Add((Condition)condition);
        }

        private async void OnSaveRule()
        {
            if (Rule.Trigger == null || !Rule.Actions.Any())
            {
                return;
            }

            Rule.Id = Guid.NewGuid().ToString();

            await Rule.SaveAsync();
            Close(this);

            if (Rule.Trigger.IsTimeTrigger)
            {
                schedulerService.Schedule(Rule.Id.GetHashCode(), Rule.Tag, Rule.Trigger.TriggerDelay);
            }
        }

        private async void OnSetTrigger()
        {
            var triggers = autobotService.GetTriggers();
            Trigger trigger = (Trigger)await presentationService.SelectFromGridAsync(triggers);
            triggers = autobotService.GetTriggers(trigger);

            if (triggers != null)
            {
                if (triggers.Count > 1)
                {
                    trigger = (Trigger)await presentationService.SelectFromListAsync(triggers);
                }
                else
                {
                    trigger = triggers.FirstOrDefault();
                }
            }

            await HandleTimeTrigger(trigger);

            Rule.Trigger = trigger;
        }
    }
}