using Autobot.Droid.Infrastructure.Repository;
using Autobot.Droid.Views;
using System.Collections.Generic;

namespace Autobot.Droid.Presenter
{
    public class ActionPickerPresenter
    {
        private readonly Repository repository;
        private readonly IActionPickerView view;

        public ActionPickerPresenter(IActionPickerView view)
        {
            this.view = view;
            repository = new Repository();
        }

        public async void RequestActions()
        {
            List<Autobot.Infrastructure.Action> actions = repository.GetActions();
            Autobot.Infrastructure.Action action = await view.PromptAsync(actions);

            actions = repository.GetActions(action);
            action = await view.PromptAsync(actions);

            view.OnActionSelected(action);
        }
    }
}