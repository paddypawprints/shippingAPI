using System;
using System.Reflection;
using System.Collections.Generic;

#if NETSTANDARD1_3
namespace PitneyBowes.Developer.ShippingApi
{
    public static class TypeExtensions
    {
        public static bool IsAssignableFrom(this Type typeTo, Type typeFrom)
        {
            return typeTo.GetTypeInfo().IsAssignableFrom(typeFrom.GetTypeInfo());
        }
        public static PropertyInfo GetProperty(this Type t, string propName)
        {
            return t.GetRuntimeProperty(propName);
        }
        public static IEnumerable<PropertyInfo> GetProperties(this Type t)
        {
            return t.GetRuntimeProperties();
        }
        public static IEnumerable<Type> GetInterfaces(this Type t)
        {
            return t.GetTypeInfo().ImplementedInterfaces;
        }
        public static Type[] GetGenericArguments(this Type t)
        {
            return t.GetTypeInfo().GenericTypeArguments;
        }
    }
}
#endif
