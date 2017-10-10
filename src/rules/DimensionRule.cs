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

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DimensionRule
    {
        [JsonProperty("required")]
        virtual public Boolean Required{get; set;}
        [JsonProperty("unitOfMeasurement")]
        virtual public UnitOfDimension UnitOfMeasurement{get; set;}
        [JsonProperty("minParcelDimensions")]
        virtual public IParcelDimension MinParcelDimensions{get; set;}
        [JsonProperty("maxParcelDimensions")]
        virtual public IParcelDimension MaxParcelDimensions{get; set;}
        [JsonProperty("minLengthPlusGirth")]
        virtual public Decimal MinLengthPlusGirth{get; set;}
        [JsonProperty("maxLengthPlusGirth")]
        virtual public Decimal MaxLengthPlusGirth{get; set;}
    }
}
