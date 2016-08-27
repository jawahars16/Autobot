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
        
        void ShowDialog<T>();

        Task<Time> PromptTime();

        Task<Date> PromptDate();

        Task<IEnumerable<WeekDay>> PromptWeekDays();

        Time GetDefaultTime();

        Date GetDefaultDate();
        
    }
}