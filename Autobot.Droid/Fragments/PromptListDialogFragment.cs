using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Autobot.Platform;
using Com.Lilarcor.Cheeseknife;
using System;
using System.Collections.Generic;

namespace Autobot.Droid.Fragments
{
    public class PromptListDialogFragment : DialogFragment
    {
        [InjectView(Resource.Id.promptList)]
        private ListView promptList;

        public Action<ISelectable> Click { get; set; }
        public IList<ISelectable> Source { get; set; }
        public string Title { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.PromptListDialogFragment, container, false);
            Cheeseknife.Inject(this, view);
            var adapter = new ArrayAdapter<ISelectable>(Activity, Android.Resource.Layout.SimpleListItem1, Source);
            promptList.Adapter = adapter;
            Dialog.SetTitle(Title);
            return view;
        }

        [InjectOnItemClick(Resource.Id.promptList)]
        private void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Dismiss();
            if (Click != null)
            {
                Click.Invoke(Source[e.Position]);
            }
        }
    }
}