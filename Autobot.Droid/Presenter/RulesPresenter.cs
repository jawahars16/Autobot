using Autobot.Common;
using Autobot.Droid.Views;
using Autobot.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autobot.Droid.Presenter
{
    public class RulesPresenter
    {
        private IRulesView view;

        public async Task<List<Rule>> GetRules()
        {
            return await Database.Default.GetRulesAsync();
        }
    }
}