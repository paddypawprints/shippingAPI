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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonSpecialServices<T> : JsonWrapper<T>, ISpecialServices where T:ISpecialServices,new()
    {
        public JsonSpecialServices() : base() { }

        public JsonSpecialServices(T t) : base(t) { }

        public JsonSpecialServices(SpecialServiceCodes c) : base()
        {
            Wrapped.SpecialServiceId = c;
        }

        [JsonProperty("specialServiceId")]
        public SpecialServiceCodes SpecialServiceId
        {
            get => Wrapped.SpecialServiceId;
            set { Wrapped.SpecialServiceId = value; }
        }
        [JsonProperty("inputParameters")]
        public IEnumerable<IParameter> InputParameters
        {
            get => Wrapped.InputParameters;
            set { Wrapped.InputParameters = value; }
        }
        public void AddParameter(IParameter p)
        {
            Wrapped.AddParameter(p);
        }
        public bool ShouldSerializeFee() => Fee > 0.0M;
        [JsonProperty("fee")]
        public decimal Fee
        {
            get => Wrapped.Fee;
            set { Wrapped.Fee = value; }
        }
        public bool ShouldSerializeValue() => false;
        public decimal Value
        {
            get
            {
                foreach (var p in InputParameters)
                {
                    if (p.Name == "INPUT_VALUE")
                    {
                        if (decimal.TryParse(p.Value, out decimal value))
                        {
                            return value;
                        }
                    }
                }
                return 0M;
            }
            set { throw new NotImplementedException(); }
        }
    }

}