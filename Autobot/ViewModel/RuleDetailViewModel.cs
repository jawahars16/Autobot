using Autobot.Infrastructure;
using Autobot.Services;
using MvvmCross.Core.ViewModels;
using System.Linq;

namespace Autobot.ViewModel
{
    public class RuleDetailViewModel : MvxViewModel
    {
        private readonly IAutobotService autobotService;
        private readonly IPresentationService presentationService;

        public RuleDetailViewModel(IAutobotService autobotService, IPresentationService presentationService)
        {
            this.autobotService = autobotService;
            this.presentationService = presentationService;
            Rule = new Rule();
            SetTriggerCommand = new MvxCommand(OnSetTrigger);
            AddConditionCommand = new MvxCommand(OnAddCondition);
            AddActionCommand = new MvxCommand(OnAddAction);
            SaveCommand = new MvxCommand(OnSaveRule);
        }

        public IMvxCommand AddActionCommand { get; set; }

        public IMvxCommand AddConditionCommand { get; set; }

        public Rule Rule { get; set; }

        public IMvxCommand SaveCommand { get; set; }

        public IMvxCommand SetTriggerCommand { get; set; }

        private async void OnAddAction()
        {
            var actions = autobotService.GetActions();
            var action = await presentationService.SelectFromGridAsync(actions);

            actions = autobotService.GetActions((Infrastructure.Action)action);
            action = await presentationService.SelectFromListAsync(actions);

            Rule.Actions.Add((Infrastructure.Action)action);
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

            await Rule.SaveAsync();
            Close(this);
        }

        private async void OnSetTrigger()
        {
            var triggers = autobotService.GetTriggers();
            var trigger = await presentationService.SelectFromGridAsync(triggers);

            triggers = autobotService.GetTriggers((Trigger)trigger);
            trigger = await presentationService.SelectFromListAsync(triggers);

            Rule.Trigger = (Trigger)trigger;
        }
    }
}