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

using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Pickup : IPickup
    {
        /// <summary>
        /// Unique transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        virtual public string TransactionId { get; set; }
        /// <summary>
        /// Gets or sets the pickup address. Required.
        /// </summary>
        /// <value>The pickup address.</value>
        virtual public IAddress PickupAddress{get; set;}
        /// <summary>
        /// Gets or sets the carrier. Only USPS supported.
        /// </summary>
        /// <value>The carrier. Only USPS supported.</value>
        virtual public Carrier Carrier{get; set;}
        /// <summary>
        /// The parcel descriptions. Each object in the array describes a group of parcels.
        /// </summary>
        /// <value>The pickup summary.</value>
        virtual public IEnumerable<IPickupCount> PickupSummary{get; set;}
        virtual public string Reference{get; set;}
        virtual public PackageLocation PackageLocation{get; set;}
        virtual public string SpecialInstructions{get; set;}
        virtual public DateTime PickupDate { get; set; }
        virtual public string PickupConfirmationNumber { get; set; }
        virtual public string PickupId { get; set; }

        public void AddPickupCount(IPickupCount p)
        {
            ModelHelper.AddToEnumerable<IPickupCount, IPickupCount>(p, () => PickupSummary, (x) => PickupSummary = x);
        }
    }
}
