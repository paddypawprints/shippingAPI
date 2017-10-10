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
        public static bool IsWithin(this IParcelDimension parcel, DimensionRule rule)
        {
            // TODO: convert to parcel units
            if (parcel.Height < rule.MinParcelDimensions.Height || parcel.Length < rule.MinParcelDimensions.Length || parcel.Width < rule.MinParcelDimensions.Width)
                return false;
            if (parcel.Height > rule.MaxParcelDimensions.Height || parcel.Length > rule.MaxParcelDimensions.Length || parcel.Width > rule.MaxParcelDimensions.Width)
                return false;
//            if (parcel.IrregularParcelGirth < rule.MinLengthPlusGirth)
//                return false;
//            if (parcel.IrregularParcelGirth > rule.MaxLengthPlusGirth)
 //               return false;
            return true;
        }
        public static bool IsWithin(this IParcelWeight parcel, WeightRule rule)
        {
            // TODO: convert to parce units
            if (parcel.Weight < rule.MinWeight) return false;
            if (parcel.Weight > rule.MaxWeight) return false;
            return true;
        }
    }
}
