using Autobot.Attributes;
using Autobot.Common;
using Autobot.Model;
using Autobot.Platform;
using Autobot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autobot.Droid.Services
{
    public class AutobotService : IAutobotService
    {
        private IReflection reflection;

        public AutobotService()
        {
            reflection = Container.Default.Resolve<IReflection>();
        }

        public List<Autobot.Model.Action> GetActions()
        {
            var types = reflection.GetTypesWithAttribute<ActionAttribute>();
            if (types != null && types.Any())
            {
                return types.Select((type) =>
                {
                    var actionAttribute = type.GetCustomAttribute<ActionAttribute>();
                    return Model.Action.Create(actionAttribute.Title, type);
                }).ToList();
            }

            return null;
        }

        public List<Model.Action> GetActions(Model.Action action)
        {
            var actionObject = Activator.CreateInstance(action.Type);
            var methods = reflection.GetMethodsWithAttribute<ActionAttribute>(action.Type);
            if (methods != null && methods.Any())
            {
                return methods.Select((method) =>
                {
                    var actionAttribute = method.GetCustomAttribute<ActionAttribute>();
                    return Model.Action.Create(actionAttribute.Title, action.Type, method, null);
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
                    return Condition.Create(triggerAttribute.Title, triggerAttribute.Icon, condition.Type, method, null);
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
                    return Trigger.Create(field.GetValue(triggerObject).ToString(), triggerAttribute.Title, triggerAttribute.Icon, trigger.Type);
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
                    return Trigger.Create(triggerAttribute.Title, triggerAttribute.Icon, type);
                }).ToList();
            }

            return null;
        }
    }
}