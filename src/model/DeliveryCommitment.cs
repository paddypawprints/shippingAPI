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

namespace PitneyBowes.Developer.ShippingApi.Model
{
    /// <summary>
    /// Time in transit for the shipment.
    /// </summary>
    public class DeliveryCommitment : IDeliveryCommitment
    {
        /// <summary>
        /// Gets or sets the minimum estimated number of days.
        /// </summary>
        /// <value>The minimum estimated number of days.</value>
        virtual public string MinEstimatedNumberOfDays { get; set;}
        /// <summary>
        /// Gets or sets the max estimated number of days.
        /// </summary>
        /// <value>The max estimated number of days.</value>
        virtual public string MaxEstimatedNumberOfDays { get; set;}
        /// <summary>
        /// Gets or sets the estimated delivery date time.
        /// </summary>
        /// <value>The estimated delivery date time.</value>
        virtual public string EstimatedDeliveryDateTime { get;set;}
        /// <summary>
        /// Whether the shipment is guaranteed by the carrier.
        /// </summary>
        /// <value>The guarantee.</value>
        virtual public string Guarantee {get;set;}
        /// <summary>
        /// These are carrier specific details that may be provided.
        /// </summary>
        /// <value>The additional details.</value>
        virtual public string AdditionalDetails {get;set;}        
    }

}