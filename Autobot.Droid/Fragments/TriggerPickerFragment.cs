using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Autobot.Droid.Platform;
using Autobot.Droid.Presenter;
using Autobot.Droid.Views;
using Autobot.Infrastructure;
using Autobot.Platform;
using Com.Lilarcor.Cheeseknife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autobot.Droid.Fragments
{
    public class TriggerPickerFragment : Fragment, ITriggerPickerView
    {
        private TriggerPickerPresenter presenter;
        private Rule rule;

        private Trigger selectedTrigger;

        [InjectView(Resource.Id.selectedTrigger)]
        private TextView selectedTriggerTxt;

        public TriggerPickerFragment()
        {
            // I have nothing, but don't delete me. :(
        }

        public TriggerPickerFragment(Rule rule)
        {
            this.rule = rule;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            presenter = new TriggerPickerPresenter(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TriggerPickerFragment, container, false);
            Cheeseknife.Inject(this, view);

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            if (selectedTrigger != null)
                OnTriggerSelected(selectedTrigger);
        }

        public void OnTriggerSelected(Trigger trigger)
        {
            selectedTrigger = trigger;
            selectedTriggerTxt.Text = trigger.Title;
            rule.Trigger = trigger;
        }

        public async Task<Trigger> PromptAsync(List<Trigger> triggers)
        {
            var trigger = await Prompt.Make(Activity, "Select trigger", triggers.Cast<ISelectable>().ToList()).ShowAsync();
            return trigger as Trigger;
        }

        [InjectOnClick(Resource.Id.selectTriggerBtn)]
        private void OnSelectTrigger(object sender, EventArgs e)
        {
            presenter.RequestTriggers();
        }
    }
}