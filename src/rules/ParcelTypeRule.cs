/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ParcelTypeRule : IRateRule
    {
        [JsonProperty("parcelType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ParcelType ParcelType { get; set; }
        [JsonProperty("brandedName")]
        public string BrandedName { get; set; }
        [JsonProperty("rateTypeId")]
        public string RateTypeId { get; set; }
        [JsonProperty("rateTypeBrandedName")]
        public string RateTypeBrandedName { get; set; }
        [JsonProperty("trackable")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Trackable Trackable { get; set; }
        public IndexedList<SpecialServiceCodes, SpecialServicesRule> SpecialServiceRules { get; set; }
        [JsonProperty("weightRules")]
        public IEnumerable<WeightRule> WeightRules { get; set; }
        [JsonProperty("dimensionRules")]
        public IEnumerable<DimensionRule> DimensionRules { get; set; }
        [JsonProperty("suggestedTrackableSpecialServiceId")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpecialServiceCodes SuggestedTrackableSpecialServiceId { get; set; }
        [JsonProperty("specialServiceRules")]
        internal IEnumerable<SpecialServicesRule> SerializerSpecialServiceRules
        {
            get => SpecialServiceRules;
            set
            {
                if (SpecialServiceRules == null) SpecialServiceRules = new IndexedList<SpecialServiceCodes, SpecialServicesRule>();
                foreach (var ss in value)
                {
                    SpecialServiceRules.Add(ss.SpecialServiceId, ss);
                }
            }
        }
        public void Accept(IRateRuleVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool FitsDimensions(IParcelDimension dimensions)
        {
            foreach (var d in DimensionRules)
            {
                if (!dimensions.IsWithin(d))
                {
                    return false;
                }
            }
            return true;
        }

        public bool HoldsWeight(IParcelWeight weight)
        {
            foreach (var w in WeightRules)
            {
                if (!weight.IsWithin(w))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
