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

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class SpecialServices : ISpecialServices
    {
        public SpecialServices()
        {
        }
        /// <summary>
        /// Gets or sets the special service identifier.
        /// </summary>
        /// <value>The special service identifier.</value>
        virtual public SpecialServiceCodes SpecialServiceId { get; set;}
        /// <summary>
        /// Gets or sets the input parameters.
        /// </summary>
        /// <value>The input parameters.</value>
        virtual public IEnumerable<IParameter> InputParameters { get; set; }
        virtual public void AddParameter(IParameter p)
        {
            ModelHelper.AddToEnumerable<IParameter, Parameter>(p, () => InputParameters, (x) => InputParameters = x);
        }
        /// <summary>
        /// Gets or sets the fee.
        /// </summary>
        /// <value>The fee.</value>
        virtual public decimal Fee { get; set;}
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        virtual public decimal Value
        {
            get
            {
                foreach( var p in InputParameters )
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
            set
            {
                foreach (var p in InputParameters)
                {
                    if (p.Name == "INPUT_VALUE")
                    {
                        p.Value = value.ToString();
                        return;
                    }
                }
                AddParameter(new Parameter() { Name = "INPUT_VALUE", Value = "0" });
            }
        }
    }
}