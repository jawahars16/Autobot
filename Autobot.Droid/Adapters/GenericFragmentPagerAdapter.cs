using Android.Support.V4.App;
using System.Collections.Generic;

namespace Autobot.Droid.Adapters
{
    public class GenericFragmentPagerAdapter : FragmentPagerAdapter
    {
        private List<Android.Support.V4.App.Fragment> _fragmentList = new List<Android.Support.V4.App.Fragment>();

        public GenericFragmentPagerAdapter(Android.Support.V4.App.FragmentManager fm)
            : base(fm) { }

        public override int Count
        {
            get { return _fragmentList.Count; }
        }

        public void AddFragment(Android.Support.V4.App.Fragment fragment)
        {
            _fragmentList.Add(fragment);
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return _fragmentList[position];
        }
    }
}