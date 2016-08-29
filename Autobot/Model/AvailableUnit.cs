using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class AvailableUnit : ISelectable
    {
        public string Title { get; set; }
        public int Icon { get; set; }
        public double Value { get; set; }

        public AvailableUnit(string title, double value)
        {
            Title = title;
            Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
