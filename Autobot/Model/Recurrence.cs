using Autobot.Common;
using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class Recurrence : ISelectable
    {
        public string Title { get; set; }

        public int Icon { get; set; }

        public string RecurrenceRule { get; set; }

        public Recurrence(string rule)
        {
            RecurrenceRule = rule;
            dynamic _rule = ParseRule();
            Title = $"{_rule.freq}";
        }

        public dynamic ParseRule()
        {
            string[] tokens = RecurrenceRule.Split(';');
            ExpandoObject expandoObject = new ExpandoObject();
            foreach (var token in tokens)
            {
                string name = token.Split('=')[0];
                string value = token.Split('=')[1];
                expandoObject.AddProperty(name, value);
            }
            return expandoObject;
        }
    }
}
