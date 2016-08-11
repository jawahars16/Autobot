using Autobot.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autobot.Droid.Views
{
    public interface ITriggerPickerView
    {
        void OnTriggerSelected(Trigger trigger);

        Task<Trigger> PromptAsync(List<Trigger> triggers);
    }
}