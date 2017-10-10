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
    public class JsonCustomsItems<T> : JsonWrapper<T>, ICustomsItems where T : ICustomsItems, new()
    {
        public JsonCustomsItems() : base() { }

        public JsonCustomsItems(T t) : base(t) { }

        [JsonProperty("description")]
        public string Description
        {
            get => Wrapped.Description;
            set { Wrapped.Description = value; }
        }
        [JsonProperty("quantity")]
        public int Quantity
        {
            get => Wrapped.Quantity;
            set { Wrapped.Quantity = value; }
        }
        [JsonProperty("unitPrice")]
        public decimal UnitPrice
        {
            get => Wrapped.UnitPrice;
            set { Wrapped.UnitPrice = value; } 
        }
        [JsonProperty("unitWeight")]
        public IParcelWeight UnitWeight
        {
            get => Wrapped.UnitWeight;
            set { Wrapped.UnitWeight = value; }
        }
        [JsonProperty("hSTariffCode")]
        public string HSTariffCode
        {
            get => Wrapped.HSTariffCode;
            set { Wrapped.HSTariffCode = value; }
        }
        [JsonProperty("originCountryCode")]
        public string OriginCountryCode
        {
            get => Wrapped.OriginCountryCode;
            set { Wrapped.OriginCountryCode = value; } 
        }
    }

}