using Autobot.Common;
using Autobot.Model;
using Autobot.ViewModel;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;

namespace Autobot.Viewmodel
{
    public class HomeViewModel : MvxViewModel
    {
        private ObservableCollection<Rule> _Rules;

        public HomeViewModel()
        {
            RuleDetailCommand = new MvxCommand<Rule>(rule => OnRuleDetail(rule));
        }

        public IMvxCommand RuleDetailCommand { get; set; }

        public ObservableCollection<Rule> Rules
        {
            get { return _Rules; }
            set { _Rules = value; RaisePropertyChanged(() => Rules); }
        }

        public void OnRuleDetail(Rule rule)
        {
            if (rule != null)
            {
                ShowViewModel<RuleDetailViewModel>(new { Id = rule?.PrimaryKey });
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