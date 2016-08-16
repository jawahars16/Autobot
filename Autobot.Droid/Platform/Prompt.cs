using Android.App;
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
            var dialog = new PromptListDialogFragment(list);
            dialog.Source = items;
            dialog.Title = title;
            dialog.Click = (item) =>
            {
                source.SetResult(item);
            };
            dialog.Show(context.FragmentManager, "");
            return source.Task;
        }
    }
}