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

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WeightRule
    {
        [JsonProperty("required")]
        public Boolean Required{get; set;}
        [JsonProperty("unitOfWeight")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfWeight UnitOfWeight{get; set;}
        [JsonProperty("minWeight")]
        public Decimal MinWeight{get; set;}
        [JsonProperty("maxWeight")]
        public Decimal MaxWeight{get; set;}
    }
}
