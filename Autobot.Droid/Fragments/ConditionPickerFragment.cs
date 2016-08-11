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
    public class ConditionPickerFragment : Fragment, IConditionPickerView
    {
        private ArrayAdapter adapter;
        private ConditionPickerPresenter presenter;
        private Rule rule;

        [InjectView(Resource.Id.selectedConditionsList)]
        private ListView selectedConditionsList;

        public ConditionPickerFragment()
        {
            // I have nothing, but don't delete me. :(
        }

        public ConditionPickerFragment(Rule rule)
        {
            this.rule = rule;
        }

        public void OnConditionSelected(Condition condition)
        {
            rule.Conditions.Add(condition);
            InitializeListView();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            presenter = new ConditionPickerPresenter(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ConditionPickerFragment, container, false);
            Cheeseknife.Inject(this, view);
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            InitializeListView();
        }

        public async Task<Condition> PromptAsync(List<Condition> conditions)
        {
            var condition = await Prompt.Make(Activity, "Select condition", conditions.Cast<ISelectable>().ToList()).ShowAsync();
            return condition as Condition;
        }

        private void InitializeListView()
        {
            if (rule.Conditions != null)
            {
                adapter = new ArrayAdapter<Condition>(Activity, Android.Resource.Layout.SimpleListItem1, rule.Conditions);
                selectedConditionsList.Adapter = adapter;
            }
        }

        [InjectOnClick(Resource.Id.selectConditionBtn)]
        private void OnClick(object sender, EventArgs e)
        {
            presenter.RequestConditions();
        }
    }
}