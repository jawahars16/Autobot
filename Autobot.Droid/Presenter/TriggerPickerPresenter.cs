using Autobot.Droid.Infrastructure.Repository;
using Autobot.Droid.Views;

namespace Autobot.Droid.Presenter
{
    public class TriggerPickerPresenter
    {
        private readonly Repository repository;
        private readonly ITriggerPickerView view;

        public TriggerPickerPresenter(ITriggerPickerView view)
        {
            this.view = view;
            repository = new Repository();
        }

        public async void RequestTriggers()
        {
            var triggers = repository.GetTriggers();
            var trigger = await view.PromptAsync(triggers);

            triggers = repository.GetTriggers(trigger);
            trigger = await view.PromptAsync(triggers);

            view.OnTriggerSelected(trigger);
        }
    }
}