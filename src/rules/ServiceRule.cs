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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Rules
{

    [JsonObject(MemberSerialization.OptIn)]
    public class ServiceRule : IRateRule
    {

        [JsonProperty("serviceId")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Services ServiceId { get; set; }
        [JsonProperty("brandedName")]
        public string BrandedName { get; set; }
        public IndexedList<ParcelType, ParcelTypeRule> ParcelTypeRules { get; internal set; }

        [JsonProperty("parcelTypeRules")]
        internal IEnumerable<ParcelTypeRule> SerializerParcelTypeRules {
            get => ParcelTypeRules;
            set
            {
                if (ParcelTypeRules == null) ParcelTypeRules = new IndexedList<ParcelType, ParcelTypeRule>();
                foreach( var r in value)
                {
                    ParcelTypeRules.Add(r.ParcelType, r);
                }
                
            }
        }

        public void Accept(IRateRuleVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
