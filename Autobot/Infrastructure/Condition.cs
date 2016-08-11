using Autobot.Common;
using Autobot.Platform;
using System;
using System.Reflection;

namespace Autobot.Infrastructure
{
    public class Condition : ISelectable
    {
        private Condition(string title, Type type, MethodInfo method, params object[] parameters)
        {
            Type = type;
            Method = method;
            Parameters = parameters;
            Title = title;
        }

        public string Icon { get; set; }
        public MethodInfo Method { get; set; }
        public object[] Parameters { get; set; }
        public string Title { get; set; }
        public Type Type { get; set; }

        public static Condition Create(string title, Type type, MethodInfo method, params object[] parameters)
        {
            return new Condition(title, type, method, parameters);
        }

        public static Condition Create(string title, Type type)
        {
            return new Condition(title, type, null, null);
        }

        public bool IsSatisfied()
        {
            IReflection reflection = Container.Default.Resolve<IReflection>();
            return reflection.ExecutePredicate(Type.AssemblyQualifiedName, Method.Name, Parameters);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}