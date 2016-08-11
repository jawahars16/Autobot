using Autobot.Droid.Infrastructure.Repository;
using Autobot.Droid.Views;
using Autobot.Infrastructure;
using System.Collections.Generic;

namespace Autobot.Droid.Presenter
{
    public class ConditionPickerPresenter
    {
        private readonly Repository repository;
        private readonly IConditionPickerView view;

        public ConditionPickerPresenter(IConditionPickerView view)
        {
            this.view = view;
            repository = new Repository();
        }

        public async void RequestConditions()
        {
            List<Condition> conditions = repository.GetConditions();
            Condition condition = await view.PromptAsync(conditions);

            conditions = repository.GetConditions(condition);
            condition = await view.PromptAsync(conditions);

            view.OnConditionSelected(condition);
        }
    }
}