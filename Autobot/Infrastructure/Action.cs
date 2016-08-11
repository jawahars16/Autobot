using Autobot.Common;
using Autobot.Platform;
using System;
using System.Reflection;

namespace Autobot.Infrastructure
{
    public class Action : ISelectable
    {
        private Action(string title, Type type, MethodInfo method, params object[] parameters)
        {
            Title = title;
            Type = type;
            Method = method;
            Parameters = parameters;
        }

        public string Icon { get; set; }
        public MethodInfo Method { get; set; }
        public object[] Parameters { get; set; }
        public string Title { get; set; }
        public Type Type { get; set; }

        public static Action Create(string title, Type type, MethodInfo method, params object[] parameters)
        {
            return new Action(title, type, method, parameters);
        }

        public static Action Create(string title, Type type)
        {
            return new Action(title, type, null, null);
        }

        public void Fire()
        {
            IReflection reflection = Container.Default.Resolve<IReflection>();
            reflection.ExecuteAction(Type.AssemblyQualifiedName, Method.Name, Parameters);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}