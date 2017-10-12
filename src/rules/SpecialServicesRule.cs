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

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SpecialServicesRule : IRateRule
    {
        [JsonProperty("specialServiceId")]
        public SpecialServiceCodes SpecialServiceId { get; set; }
        [JsonProperty("brandedName")]
        public string BrandedName { get; set; }
        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
        [JsonProperty("trackable")]
        public bool Trackable { get; set; }
        public Dictionary<string, ServicesParameterRule> InputParameterRules { get; set; }
        public IndexedList<SpecialServiceCodes, ServicesPrerequisiteRule> PrerequisiteRules { get; set; }
        [JsonProperty("incompatibleSpecialServices")]
        public IEnumerable<SpecialServiceCodes> IncompatibleSpecialServices { get; set; }
        [JsonProperty("inputParameterRules")]
        internal IEnumerable<ServicesParameterRule> SerializerInputParameterRules
        {
            get
            {
                if (InputParameterRules == null) return null;
                else return InputParameterRules.Values;
            }
            set
            {
                if (InputParameterRules == null) InputParameterRules = new Dictionary<string, ServicesParameterRule>();
                foreach(var p in value)
                {
                    InputParameterRules.Add(p.Name, p);
                }
            }
        }
        [JsonProperty("prerequisiteRules")]
        internal IEnumerable<ServicesPrerequisiteRule> SerializerPrerequisiteRules
        {
            get => PrerequisiteRules;
            set
            {
                if (PrerequisiteRules == null) PrerequisiteRules = new IndexedList<SpecialServiceCodes, ServicesPrerequisiteRule>();
                foreach(var p in value)
                {
                    PrerequisiteRules.Add(p.SpecialServiceId, p);
                }
            }
        }

        public void Accept(IRateRuleVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsValidParameterValues(ISpecialServices services)
        {
            foreach (var ip in services.InputParameters)
            {
                if (!InputParameterRules.ContainsKey(ip.Name)) return false;
                if (decimal.TryParse(ip.Value, out decimal value))
                {
                    if (value < InputParameterRules[ip.Name].MinValue) return false;
                    if (value > InputParameterRules[ip.Name].MaxValue) return false;
                }
            }
             return true;
        }

        public bool HasRequiredParameters(ISpecialServices services)
        {
            Dictionary<string, bool> foundRequired = new Dictionary<string, bool>();
            foreach (var p in InputParameterRules.Values)
            {
                if (p.Required) foundRequired.Add(p.Name, false);
            }
            foreach (var ip in services.InputParameters)
            {
                if (!InputParameterRules.ContainsKey(ip.Name)) return false;
                if (foundRequired.ContainsKey(ip.Name)) foundRequired[ip.Name] = true;
            }
            foreach (var f in foundRequired)
            {
                if (!f.Value) return false;
            }
            return true;
        }

        public bool IsValidPrerequisites(IEnumerable<ISpecialServices> services)
        {
            if (PrerequisiteRules == null) return true;
            foreach(var ss in services)
            {
                if (!PrerequisiteRules.ContainsKey(ss.SpecialServiceId)) continue;
                foreach(var r in PrerequisiteRules[ss.SpecialServiceId])
                {
                    if ( ss.Value < r.MinInputValue ) return false;
                }
            }
            return true;
        }
    }
}
