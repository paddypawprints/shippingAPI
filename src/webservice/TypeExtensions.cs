/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

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
