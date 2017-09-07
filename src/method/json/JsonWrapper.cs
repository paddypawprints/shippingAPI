using System;
using System.Reflection;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    public abstract class JsonWrapper<T> where T: new()
    {
        public JsonWrapper()
        {
            Wrapped = new T();
        }

        public JsonWrapper( T t)
        {
            Wrapped = t;
        }

        public static explicit operator T(JsonWrapper<T> j)
        {
            return j.Wrapped;
        }

        public virtual T Wrapped { get; set; }

     }
}
