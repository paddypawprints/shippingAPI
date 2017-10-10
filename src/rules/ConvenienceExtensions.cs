/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

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
