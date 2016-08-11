using System.Collections.Generic;
using System.Linq;

namespace Autobot.Infrastructure
{
    public class Rule
    {
        public Rule()
        {
            Conditions = new List<Condition>();
            Actions = new List<Action>();
        }

        public List<Action> Actions { get; set; }
        public List<Condition> Conditions { get; set; }
        public Trigger Trigger { get; set; }

        public void Run()
        {
            if (Conditions.All(condition => condition.IsSatisfied()))
            {
                foreach (var action in Actions)
                {
                    action.Fire();
                }
            }
        }
    }
}