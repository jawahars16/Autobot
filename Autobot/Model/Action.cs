﻿using Autobot.Common;
using Autobot.Platform;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Autobot.Model
{
    public class Action : ISelectable
    {
        public Action()
        {
            // Don't kill me. I serve purpose for SQLite.
        }

        private Action(string title, int icon, Type type, MethodInfo method, params object[] parameters)
        {
            Title = title;
            Icon = icon;
            Type = type;
            Method = method;
            Parameters = parameters;
        }

        #region Serializable

        [PrimaryKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Icon { get; set; }
        public string MethodName { get; set; }
        public string ParameterList { get; set; }
        public string Rule { get; set; }
        public string TypeName { get; set; }
        public string Tag { get; set; }

        #endregion Serializable

        [Ignore]
        public MethodInfo Method { get; set; }

        [Ignore]
        public object[] Parameters { get; set; }

        [Ignore]
        public Type Type { get; set; }

        public static Action Create(string title,int icon, Type type, MethodInfo method, params object[] parameters)
        {
            return new Action(title, icon, type, method, parameters);
        }

        public static Action Create(string title, int icon, Type type)
        {
            return new Action(title, icon, type, null, null);
        }

        public void Fire()
        {
            IReflection reflection = Container.Default.Resolve<IReflection>();
            reflection.ExecuteAction(Type.AssemblyQualifiedName, Method.Name, Parameters);
        }

        public void Load()
        {
            Type = System.Type.GetType(TypeName);
            Method = Type.GetRuntimeMethods().Where(method => method.Name == MethodName).FirstOrDefault();
        }

        public async Task SaveAsync(Rule rule)
        {
            Id = Guid.NewGuid().ToString();
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