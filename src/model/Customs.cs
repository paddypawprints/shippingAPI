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

namespace PitneyBowes.Developer.ShippingApi.Model
{
    /// <summary>
    /// Customs clearance information that is used to fill out a commercial invoice
    /// </summary>
    public class Customs : ICustoms
    {
        public Customs()
        {
            CustomsItems = new List<CustomsItems>();
        }
        virtual public ICustomsInfo CustomsInfo { get; set;}
        /// <summary>
        /// The commodity information about each item in an international shipment
        /// used for customs clearance.
        /// The maximum number of objects in the array is **30**.
        /// </summary>
        /// <value>The customs items.</value>
        virtual public IEnumerable<ICustomsItems> CustomsItems { get; set; }
        virtual public ICustomsItems AddCustomsItems(ICustomsItems c)
        {
            return ModelHelper.AddToEnumerable<ICustomsItems, CustomsItems>(c, () => CustomsItems, (x) => CustomsItems = x);
        }
    }
}