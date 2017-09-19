using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    internal class ModelHelper
    {
        static internal I AddToEnumerable<I,T>(I i, Func<IEnumerable<I>> get, Action<IEnumerable<I>> set)
            where T: class
            where I :class
        {
            if ((i as T) == null) throw new NullReferenceException(string.Format("Cant add null to Enumerable {0}, {1}", typeof(T).Name, typeof(I).Name)); 
            List<T> l;
            if (get() == null)
            {
                l = new List<T>();
                set( l as IEnumerable<I>);
            }
            l = get() as List<T>;
            l.Add(i as T);
            return i;
        }

    }
}
