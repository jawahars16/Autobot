using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;

namespace Autobot.Droid.Fragments
{
    public class PromptTextDialogFragment : DialogFragment
    {
        private string title;
        private string description;

        [InjectView(Resource.Id.editText)]
        private EditText editText;

        [InjectView(Resource.Id.titleText)]
        private TextView titleText;

        public event EventHandler Done;

        public string Result { get; set; }

        public PromptTextDialogFragment(string text, string description)
        {
            this.title = text;
            this.description = description;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            Dialog dialog = base.OnCreateDialog(savedInstanceState);
            dialog.SetTitle(title);
            dialog.Window.ClearFlags(WindowManagerFlags.NotFocusable | WindowManagerFlags.AltFocusableIm);
            dialog.Window.SetSoftInputMode(SoftInput.StateVisible);
            return dialog;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_PromptDialog, container, false);
            Cheeseknife.Inject(this, view);
            
            titleText.Text = description;
            editText.SetImeActionLabel("Done", Android.Views.InputMethods.ImeAction.Done);
            editText.EditorAction += OnEditorAction;

            return view;
        }

        private void OnEditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (Done != null)
            {
                Result = editText.Text;
                Done(sender, e);
                Dismiss();
            }
        }

        [InjectOnClick(Resource.Id.doneButton)]
        private void OnDoneClick(object sender, EventArgs e)
        {
            if (Done != null)
            {
                Result = editText.Text;
                Done(sender, e);
                Dismiss();
            }
        }
    }
}