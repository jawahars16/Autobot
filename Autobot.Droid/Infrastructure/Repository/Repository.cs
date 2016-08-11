using Autobot.Attributes;
using Autobot.Common;
using Autobot.Infrastructure;
using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autobot.Droid.Infrastructure.Repository
{
    public class Repository
    {
        private IReflection reflection;

        public Repository()
        {
            reflection = Container.Default.Resolve<IReflection>();
        }

        public List<Autobot.Infrastructure.Action> GetActions()
        {
            var types = reflection.GetTypesWithAttribute<ActionAttribute>();
            if (types != null && types.Any())
            {
                return types.Select((type) =>
                {
                    var actionAttribute = type.GetCustomAttribute<ActionAttribute>();
                    return Autobot.Infrastructure.Action.Create(actionAttribute.Title, type);
                }).ToList();
            }

            return null;
        }

        public List<Autobot.Infrastructure.Action> GetActions(Autobot.Infrastructure.Action action)
        {
            var actionObject = Activator.CreateInstance(action.Type);
            var methods = reflection.GetMethodsWithAttribute<ActionAttribute>(action.Type);
            if (methods != null && methods.Any())
            {
                return methods.Select((method) =>
                {
                    var actionAttribute = method.GetCustomAttribute<ActionAttribute>();
                    return Autobot.Infrastructure.Action.Create(actionAttribute.Title, action.Type, method, null);
                }).ToList();
            }

            return null;
        }

        public List<Condition> GetConditions(Condition condition)
        {
            var triggerObject = Activator.CreateInstance(condition.Type);
            var methods = reflection.GetMethodsWithAttribute<ConditionAttribute>(condition.Type);
            if (methods != null && methods.Any())
            {
                return methods.Select((method) =>
                {
                    var triggerAttribute = method.GetCustomAttribute<ConditionAttribute>();
                    return Condition.Create(triggerAttribute.Title, condition.Type, method, null);
                }).ToList();
            }

            return null;
        }

        public List<Condition> GetConditions()
        {
            var types = reflection.GetTypesWithAttribute<ConditionAttribute>();
            if (types != null && types.Any())
            {
                return types.Select((type) =>
                {
                    var triggerAttribute = type.GetCustomAttribute<ConditionAttribute>();
                    return Condition.Create(triggerAttribute.Title, type);
                }).ToList();
            }

            return null;
        }

        public List<Trigger> GetTriggers(Trigger trigger)
        {
            var triggerObject = Activator.CreateInstance(trigger.Type);
            var fields = reflection.GetFieldsWithAttribute<TriggerAttribute>(trigger.Type);
            if (fields != null && fields.Any())
            {
                return fields.Select((field) =>
                {
                    var triggerAttribute = field.GetCustomAttribute<TriggerAttribute>();
                    return Trigger.Create(field.GetValue(triggerObject).ToString(), triggerAttribute.Title);
                }).ToList();
            }

            return null;
        }

        public List<Trigger> GetTriggers()
        {
            var types = reflection.GetTypesWithAttribute<TriggerAttribute>();
            if (types != null && types.Any())
            {
                return types.Select((type) =>
                {
                    var triggerAttribute = type.GetCustomAttribute<TriggerAttribute>();
                    return Trigger.Create(triggerAttribute.Title, type);
                }).ToList();
            }

            return null;
        }
    }
}