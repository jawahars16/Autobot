using Autobot.Common;
using Autobot.Model;
using Autobot.ViewModel;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using System;
using Autobot.Services;

namespace Autobot.Viewmodel
{
    public class HomeViewModel : MvxViewModel
    {
        private ObservableCollection<Rule> _Rules;
        private readonly IPresentationService presentationService;

        public HomeViewModel(IPresentationService presentationService)
        {
            this.presentationService = presentationService;
            RuleDetailCommand = new MvxCommand<Rule>(rule => OnRuleDetail(rule));
            MenuCommand = new MvxCommand(OnMenu);
        }

        private async void OnMenu()
        {
            var item = await presentationService.RequestNavigation();
            ShowViewModel(item.Target);
        }

        public IMvxCommand RuleDetailCommand { get; set; }
        public IMvxCommand MenuCommand { get; set; }

        public ObservableCollection<Rule> Rules
        {
            get { return _Rules; }
            set { _Rules = value; RaisePropertyChanged(() => Rules); }
        }

        public void OnRuleDetail(Rule rule)
        {
            if (rule != null)
            {
                ShowViewModel<RuleDetailViewModel>(new { Id = rule?.Id });
            }
            else
            {
                ShowViewModel<RuleDetailViewModel>();
            }
        }

        public async void Resume()
        {
            var rules = await Database.Default.GetRulesAsync();
            Rules = new ObservableCollection<Rule>(rules);
        }
    }
}