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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonPickupCount<T> : JsonWrapper<T>, IPickupCount where T : IPickupCount, new()
    {
        public JsonPickupCount() : base() { }
        public JsonPickupCount(T t) : base(t) { }

        [JsonProperty("serviceId")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Services ServiceId
        {
            get => Wrapped.ServiceId;
            set { Wrapped.ServiceId = value; }
        }
        [JsonProperty("count")]
        public int Count
        {
            get => Wrapped.Count;
            set { Wrapped.Count = value; }
        }
        [JsonProperty("totalWeight")]
        public IParcelWeight TotalWeight
        {
            get => Wrapped.TotalWeight;
            set { Wrapped.TotalWeight = value; }
        }
    }
}
