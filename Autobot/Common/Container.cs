using System;
using System.Collections.Generic;

namespace Autobot.Common
{
    public class Container
    {
        private static Container instance;
        private Dictionary<Type, Type> registeredTypes = new Dictionary<Type, Type>();

        public static Container Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new Container();
                }

                return instance;
            }
        }

        public void Register<T>(Type type)
        {
            if (!registeredTypes.ContainsKey(typeof(T)))
            {
                registeredTypes.Add(typeof(T), type);
            }
        }

        public T Resolve<T>()
        {
            Type type = registeredTypes[typeof(T)];
            return (T)Activator.CreateInstance(type);
        }
    }
}