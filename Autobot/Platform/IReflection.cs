using System;
using System.Collections.Generic;
using System.Reflection;

namespace Autobot.Platform
{
    public interface IReflection
    {
        void ExecuteAction(string type, string method, object[] parameters);

        bool ExecutePredicate(string type, string method, object[] parameters);

        List<FieldInfo> GetFieldsWithAttribute<T>(Type type);

        List<MethodInfo> GetMethodsWithAttribute<T>(Type type);

        List<TypeInfo> GetTypesWithAttribute<T>();
    }
}