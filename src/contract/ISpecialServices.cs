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

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ISpecialServices
    {
        /// <summary>
        /// Gets or sets the special service identifier.
        /// </summary>
        /// <value>The special service identifier.</value>
        SpecialServiceCodes SpecialServiceId { get; set; }
        /// <summary>
        /// Gets or sets the input parameters.
        /// </summary>
        /// <value>The input parameters.</value>
        IEnumerable<IParameter> InputParameters { get; set; }
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="p">Parameter.</param>
        void AddParameter(IParameter p);
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        decimal Value { get; set; }
        /// <summary>
        /// Gets or sets the fee.
        /// </summary>
        /// <value>The fee.</value>
        decimal Fee { get; set; }
    }
}