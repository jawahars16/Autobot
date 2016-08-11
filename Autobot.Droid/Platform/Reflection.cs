using Autobot.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autobot.Droid.Platform
{
    public class Reflection : IReflection
    {
        public void ExecuteAction(string typeName, string methodName, object[] parameters)
        {
            Type type = Type.GetType(typeName);
            MethodInfo method = type.GetMethod(methodName);
            var resolvedObject = Activator.CreateInstance(type);
            method.Invoke(resolvedObject, parameters);
        }

        public bool ExecutePredicate(string typeName, string methodName, object[] parameters)
        {
            Type type = Type.GetType(typeName);
            MethodInfo method = type.GetMethod(methodName);
            var resolvedObject = Activator.CreateInstance(type);
            return (bool)method.Invoke(resolvedObject, parameters);
        }

        public List<FieldInfo> GetFieldsWithAttribute<T>(Type type)
        {
            return type.GetFields().
                Where(field => Attribute.GetCustomAttributes(field).
                Any(attribute => attribute is T)).ToList();
        }

        public List<MethodInfo> GetMethodsWithAttribute<T>(Type type)
        {
            return type.GetMethods().
                Where(method => Attribute.GetCustomAttributes(method).
                Any(attribute => attribute is T)).ToList();
        }

        public List<TypeInfo> GetTypesWithAttribute<T>()
        {
            return Assembly.GetExecutingAssembly().DefinedTypes.
                Where(type => Attribute.GetCustomAttributes(type).
                Any(attribute => attribute is T)).ToList();
        }
    }
}