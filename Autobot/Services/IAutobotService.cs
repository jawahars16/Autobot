using Autobot.Model;
using System.Collections.Generic;

namespace Autobot.Services
{
    public interface IAutobotService
    {
        List<Action> GetActions();

        List<Action> GetActions(Action action);

        List<Condition> GetConditions(Condition condition);

        List<Condition> GetConditions();

        List<Trigger> GetTriggers(Trigger trigger);

        List<Trigger> GetTriggers();
    }
}