using Autobot.Common;
using Autobot.Infrastructure;
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
            NewRuleCommand = new MvxCommand(OnNewRule);
        }

        public IMvxCommand NewRuleCommand { get; set; }

        public ObservableCollection<Rule> Rules
        {
            get { return _Rules; }
            set { _Rules = value; RaisePropertyChanged(() => Rules); }
        }

        public void OnNewRule()
        {
            ShowViewModel<RuleDetailViewModel>();
        }

        public async override void Start()
        {
            base.Start();
            var rules = await Database.Default.GetRulesAsync();
            Rules = new ObservableCollection<Rule>(rules);
        }
    }
}