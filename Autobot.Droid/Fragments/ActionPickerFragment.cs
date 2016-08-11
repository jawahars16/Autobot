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
    public class ActionPickerFragment : Fragment, IActionPickerView
    {
        private ArrayAdapter adapter;
        private ActionPickerPresenter presenter;
        private Rule rule;

        [InjectView(Resource.Id.selectedActionsList)]
        private ListView selectedActionList;

        public ActionPickerFragment()
        {
            // I have nothing, but don't delete me. :(
        }

        public ActionPickerFragment(Rule rule)
        {
            this.rule = rule;
        }

        public event EventHandler OnFinish;

        public void OnActionSelected(Autobot.Infrastructure.Action action)
        {
            rule.Actions.Add(action);
            InitializeListView();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            presenter = new ActionPickerPresenter(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ActionPickerFragment, container, false);
            Cheeseknife.Inject(this, view);
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            InitializeListView();
        }

        public async Task<Autobot.Infrastructure.Action> PromptAsync(List<Autobot.Infrastructure.Action> actions)
        {
            var action = await Prompt.Make(Activity, "Select action", actions.Cast<ISelectable>().ToList()).ShowAsync();
            return action as Autobot.Infrastructure.Action;
        }

        private void InitializeListView()
        {
            if (rule.Actions != null)
            {
                adapter = new ArrayAdapter<Autobot.Infrastructure.Action>(Activity, Android.Resource.Layout.SimpleListItem1, rule.Actions);
                selectedActionList.Adapter = adapter;
            }
        }

        [InjectOnClick(Resource.Id.finishRule)]
        private void OnFinishRule(object sender, EventArgs e)
        {
            OnFinish?.Invoke(this, e);
        }

        [InjectOnClick(Resource.Id.selectActionBtn)]
        private void OnSelectAction(object sender, EventArgs e)
        {
            presenter.RequestActions();
        }
    }
}