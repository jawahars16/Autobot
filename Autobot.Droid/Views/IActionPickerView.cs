using System.Collections.Generic;
using System.Threading.Tasks;
using Autobot.Infrastructure;

namespace Autobot.Droid.Views
{
    public interface IActionPickerView
    {
        Task<Action> PromptAsync(List<Action> actions);
        void OnActionSelected(Action action);
    }
}