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
    public class JsonAutoRefill<T> : JsonWrapper<T>, IAutoRefill where T : IAutoRefill, new()
    {
        public JsonAutoRefill() : base() { }
        public JsonAutoRefill(T t) : base(t) { }

        [JsonProperty("merchantID")]
        public string MerchantID
        {
            get => Wrapped.MerchantID;
            set { Wrapped.MerchantID = value; }
        }
        [JsonProperty("threshold")]
        public decimal Threshold
        {
            get => Wrapped.Threshold;
            set { Wrapped.Threshold = value; }
        }
        [JsonProperty("addAmount")]
        public decimal AddAmount
        {
            get => Wrapped.AddAmount;
            set { Wrapped.AddAmount = value; }
        }
        [JsonProperty("enabled")]
        public Boolean Enabled
        {
            get => Wrapped.Enabled;
            set { Wrapped.Enabled = value; }
        }
    }
}
