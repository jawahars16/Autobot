using Android.App;
using Android.Widget;
using Autobot.Droid.Fragments;
using Autobot.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autobot.Droid.Platform
{
    public class Prompt
    {
        private Activity context;
        private IEnumerable<ISelectable> items;
        private string title;

        private Prompt()
        {
            // Nobody creates me.
        }

        public static Prompt Make(Activity context, IEnumerable<ISelectable> source)
        {
            var prompt = new Prompt();
            prompt.context = context;
            prompt.items = source;
            return prompt;
        }

        public Task<ISelectable> ShowAsync(bool list = true)
        {
            var source = new TaskCompletionSource<ISelectable>();
            var dialog = new PromptListDialogFragment(list, ChoiceMode.Single);
            dialog.Source = items;
            dialog.Title = title;
            dialog.Click = (item) =>
            {
                source.SetResult(item);
            };
            dialog.Show(context.FragmentManager, "");
            return source.Task;
        }

        public Task<List<ISelectable>> ShowMultipleAsync()
        {
            List<ISelectable> selectedItems = new List<ISelectable>();

            var source = new TaskCompletionSource<List<ISelectable>>();
            var dialog = new PromptListDialogFragment(true, ChoiceMode.Multiple);
            dialog.Source = items;
            dialog.Title = title;
            dialog.Click = (item) =>
            {
                if (selectedItems.Contains(item))
                {
                    selectedItems.Remove(item);
                }
                else
                {
                    selectedItems.Add(item);
                }
            };
            dialog.Done = () =>
            {
                source.SetResult(selectedItems);
            };
            dialog.Show(context.FragmentManager, "");
            return source.Task;
        }
    }
}