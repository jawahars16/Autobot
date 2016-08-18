using Autobot.Common;
using Autobot.Platform;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class Condition : ISelectable
    {
        public Condition()
        {
            // Don't kill me. I serve purpose for SQLite.
        }

        private Condition(string title, Type type, MethodInfo method, params object[] parameters)
        {
            Type = type;
            Method = method;
            Parameters = parameters;
            Title = title;
        }

        public Condition(string title, int icon, Type type, MethodInfo method, object[] parameters)
        {
            Title = title;
            Icon = icon;
            Type = type;
            Method = method;
            Parameters = parameters;
        }

        #region Serializable

        public string Description { get; set; }
        public int Icon { get; set; }
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

        public static Condition Create(string title, int icon, Type type, MethodInfo method, params object[] parameters)
        {
            return new Condition(title, icon, type, method, parameters);
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

        public async Task SaveAsync(Rule rule)
        {
            TypeName = Type.AssemblyQualifiedName;
            MethodName = Method.Name;
            ParameterList = JsonConvert.SerializeObject(Parameters);
            Rule = rule.Id;

            await Database.Default.SaveAsync(this);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}