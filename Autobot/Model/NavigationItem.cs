using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class NavigationItem : ISelectable
    {
        public string Title { get; set; }
        public int Icon { get; set; }
        public Type Target { get; set; }

        public NavigationItem(string title, int icon, Type target)
        {
            Title = title;
            Icon = icon;
            Target = target;
        }
    }
}
