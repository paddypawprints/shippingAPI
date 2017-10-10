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
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonParcelWeight<T> : JsonWrapper<T>, IParcelWeight where T : IParcelWeight, new()
    {
        public JsonParcelWeight() : base() { }

        public JsonParcelWeight(T t) : base(t) { }

        public JsonParcelWeight( decimal w, UnitOfWeight u = UnitOfWeight.OZ) : base()
        {
            Weight = w;
            UnitOfMeasurement = u;
        }
        [JsonProperty(PropertyName ="weight", Order = 1)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Weight
        {
            get => Wrapped.Weight;
            set { Wrapped.Weight = value; }
        }
        [JsonProperty(PropertyName ="unitOfMeasurement", Order = 0)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfWeight UnitOfMeasurement
        {
            get => Wrapped.UnitOfMeasurement;
            set { Wrapped.UnitOfMeasurement = value; }
        }
    }
}