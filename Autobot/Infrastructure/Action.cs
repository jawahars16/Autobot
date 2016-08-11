using Autobot.Common;
using Autobot.Platform;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Autobot.Infrastructure
{
    public class Action : ISelectable
    {
        public Action()
        {
            // Don't kill me. I serve purpose for SQLite.
        }

        private Action(string title, Type type, MethodInfo method, params object[] parameters)
        {
            Title = title;
            Type = type;
            Method = method;
            Parameters = parameters;
        }

        #region Serializable

        public string Description { get; set; }
        public string Icon { get; set; }
        public string MethodName { get; set; }
        public string ParameterList { get; set; }
        public string Rule { get; set; }
        public string Title { get; set; }
        public string TypeName { get; set; }

        #endregion Serializable

        [Ignore]
        public MethodInfo Method { get; set; }

        [Ignore]
        public object[] Parameters { get; set; }

        [Ignore]
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

        public async Task SaveAsync(Rule rule)
        {
            Rule = rule.Id;
            MethodName = Method.Name;
            TypeName = Type.AssemblyQualifiedName;
            ParameterList = JsonConvert.SerializeObject(Parameters);

            await Database.Default.SaveAsync(this);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}