using System;
using System.Collections.Generic;

namespace Demo
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Services[typeof(T)] = service;
        }

        public static T GetService<T>()
        {
            if (Services.TryGetValue(typeof(T), out var service))
            {
                return (T) service;
            }

            throw new InvalidOperationException($"Failed to resolve service of type {typeof(T)}");
        }

        public static void Reset()
        {
            Services.Clear();
        }
    }
}