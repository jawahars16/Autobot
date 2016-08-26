using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Autobot.Platform;
using Com.Lilarcor.Cheeseknife;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autobot.Droid.Fragments
{
    public class PromptListDialogFragment : DialogFragment
    {
        private bool isList;
        private ChoiceMode mode;

        [InjectView(Resource.Id.promptList)]
        private AbsListView promptList;

        [InjectView(Resource.Id.done)]
        private Button doneBtn;

        public PromptListDialogFragment(bool isList, ChoiceMode mode)
        {
            this.isList = isList;
            this.mode = mode;
        }

        public Action<ISelectable> Click { get; set; }
        public Action Done { get; set; }
        public IEnumerable<ISelectable> Source { get; set; }
        public string Title { get; set; }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            Dialog dialog = base.OnCreateDialog(savedInstanceState);
            dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            return dialog;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            int layout = isList ? Resource.Layout.PromptListDialogFragment : Resource.Layout.PromptGridDialogFragment;
            var view = inflater.Inflate(layout, container, false);
            Cheeseknife.Inject(this, view);

            BaseAdapter<ISelectable> adapter = isList ? (BaseAdapter<ISelectable>)new ListViewAdapter(Activity, Source.ToList()) : new GridViewAdapter(Activity, Source.ToList());
            promptList.Adapter = adapter;
            promptList.ChoiceMode = mode;
            
            if(mode == ChoiceMode.Multiple)
            {
                doneBtn.Visibility = ViewStates.Visible;
                promptList.Adapter = new ListViewAdapter(Activity, Source.ToList(), mode);
            }

            return view;
        }

        [InjectOnItemClick(Resource.Id.promptList)]
        private void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (mode == ChoiceMode.Single)
            {
                Dismiss();
            }
            else
            {
                var holder = new ListViewAdapter.ViewHolder(e.View);
                if(holder != null)
                {
                    var checkedView = (CheckedTextView)holder.Title;
                    checkedView.Checked = !checkedView.Checked;
                }
            }

            if (Click != null)
            {
                Click.Invoke(Source.ToList()[e.Position]);
            }
        }

        [InjectOnClick(Resource.Id.done)]
        private void OnDoneClick(object sender, EventArgs e)
        {
            Dismiss();
            if (Done != null)
            {
                Done.Invoke();
            }
        }

        public class GridViewAdapter : BaseAdapter<ISelectable>
        {
            private Activity context;
            private List<ISelectable> items;

            public GridViewAdapter(Activity context, List<ISelectable> items) : base()
            {
                this.items = items;
                this.context = context;
            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override ISelectable this[int position]
            {
                get { return items[position]; }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View view, ViewGroup parent)
            {
                ISelectable item = items[position];
                ViewHolder holder = null;

                if (view != null)
                    holder = view.Tag as ViewHolder;

                if (holder == null)
                {
                    view = context.LayoutInflater.Inflate(Resource.Layout.SelectableGridItem, null);
                    holder = new ViewHolder(view);
                    view.Tag = holder;
                }

                holder.Title.Text = item.Title;
                int icon = item.Icon > 0 ? item.Icon : Resource.Drawable.trigger;
                holder.Image.SetImageResource(icon);

                return view;
            }

            public class ViewHolder : Java.Lang.Object
            {
                [InjectView(Resource.Id.icon)]
                public ImageView Image;

                [InjectView(Resource.Id.title)]
                public TextView Title;

                public ViewHolder(View view)
                {
                    Cheeseknife.Inject(this, view);
                }
            }
        }

        public class ListViewAdapter : BaseAdapter<ISelectable>
        {
            private Activity context;
            private List<ISelectable> items;
            private ChoiceMode mode;

            public ListViewAdapter(Activity context, List<ISelectable> items, ChoiceMode mode = ChoiceMode.Single) : base()
            {
                this.items = items;
                this.context = context;
                this.mode = mode;
            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override ISelectable this[int position]
            {
                get { return items[position]; }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View view, ViewGroup parent)
            {
                ISelectable item = items[position];
                ViewHolder holder = null;

                if (view != null)
                    holder = view.Tag as ViewHolder;

                if (holder == null)
                {
                    int layout = mode == ChoiceMode.Single ? Resource.Layout.SelectableListItem : Resource.Layout.li_MultiSelctableItem;
                    view = context.LayoutInflater.Inflate(layout, null);
                    holder = new ViewHolder(view);
                    view.Tag = holder;
                }

                holder.Title.Text = item.Title;
                if (item.Icon > 0 && mode == ChoiceMode.Single)
                {
                    holder.Image.SetImageDrawable(context.GetDrawable(item.Icon));
                }

                return view;
            }

            public class ViewHolder : Java.Lang.Object
            {
                [InjectView(Resource.Id.icon)]
                public ImageView Image;

                [InjectView(Resource.Id.title)]
                public TextView Title;

                public ViewHolder(View view)
                {
                    Cheeseknife.Inject(this, view);
                }
            }
        }
    }
}