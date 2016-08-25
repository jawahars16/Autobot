using Autobot.Model;
using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autobot.Services
{
    public interface IPresentationService
    {
        Task<ISelectable> SelectFromGridAsync(IEnumerable<ISelectable> source);

        Task<ISelectable> SelectFromListAsync(IEnumerable<ISelectable> source);

        bool IsTimeTrigger(Trigger trigger);

        void ShowDialog<T>();

        Task<Time> PromptTime();

        Task<Date> PromptDate();

        Time GetDefaultTime();

        Date GetDefaultDate();

        Task<Recurrence> PromptRecurrence(string rule);
    }
}