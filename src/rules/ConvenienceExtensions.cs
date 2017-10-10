using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public static class ConvenienceExtensions
    {
        public static bool Contains<T>(this IEnumerable<T> enumerable, T value)
        {
            foreach( var t in enumerable)
            {
                if (t == null) return value == null; 
                else if (t.Equals(value)) return true;
            }
            return false;
        }
    }
}
