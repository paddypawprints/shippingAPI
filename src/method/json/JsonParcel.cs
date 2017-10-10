/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonParcel<T> : JsonWrapper<T>, IParcel where T:IParcel, new()
    {

        public JsonParcel() : base() { }

        public JsonParcel(T t) : base(t) { }

        [JsonProperty("dimension", Order = 1)]
        public IParcelDimension Dimension
        {
            get => Wrapped.Dimension;
            set { Wrapped.Dimension = value; }
        }

        [JsonProperty("weight", Order = 0)]
        virtual public IParcelWeight Weight
        {
            get => Wrapped.Weight;
            set { Wrapped.Weight = value; }
        }

        public bool ShouldSerializeValueOfGoods() { return ValueOfGoods > 0.0M;  }
        [JsonProperty("valueOfGoods", Order = 2)]
        public decimal ValueOfGoods
        {
            get => Wrapped.ValueOfGoods;
            set { Wrapped.ValueOfGoods = value;  }
        }
        public bool ShouldSerializeCurrencyCode() { return ValueOfGoods > 0.0M; }
        [JsonProperty("currencyCode", Order = 3)]
        public string CurrencyCode
        {
            get => Wrapped.CurrencyCode;
            set { Wrapped.CurrencyCode = value; }
        }
    }


}