using Android.OS;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;

namespace Autobot.Droid.Fragments
{
    public class BindableFragment : MvxFragment
    {
        private int layout;

        public BindableFragment(int layout)
        {
            this.layout = layout;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(layout, container);
        }
    }
}