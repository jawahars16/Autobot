using Autobot.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autobot.Droid.Views
{
    public interface IConditionPickerView
    {
        Task<Condition> PromptAsync(List<Condition> conditions);
        void OnConditionSelected(Condition condition);
    }
}