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


using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;


namespace PitneyBowes.Developer.ShippingApi.Method
{
    /// <summary>
    /// Pickup cancel request.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PickupCancelRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }

        /// <summary>
        /// Gets or sets the authorization. Set automatically.
        /// </summary>
        /// <value>The authorization.</value>
        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        /// <summary>
        /// Gets or sets the pickup identifier.
        /// </summary>
        /// <value>The pickup identifier.</value>
        public string PickupId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public static class PickupMethods
    {
        /// <summary>
        /// This operation schedules a USPS package pickup from a residential or
        /// commercial location and provides a pickup confirmation number.
        /// Things to Consider:
        /// 1. USPS will pick up a merchant's packages on the next USPS delivery day (Monday
        ///    thru Saturday, excluding holidays) for free.
        /// 2. Pickup requests and cancellations must be submitted before 3:00 AM EST on
        ///    the day of pickup.
        /// 3. Before you schedule a pickup, it is recommended you use USPS's
        ///    package-pickup-api to confirm that service to the pickup address is available.
        ///      <a href="https://www.usps.com/business/web-tools-apis/package-pickup-api.htm#_Toc450550246" target="_blank"> Package Pickup Availability Web Tool</a>
        /// 4. For a valid request, you must have at least one of the following: a
        ///    Priority Mail Express package, a Priority Mail package, or an international package.
        /// 5. The address entered in the pickup request might look different in the
        ///    pickup response. This is because USPS standardizes and verifies addresses
        ///    to make certain that carriers can find the pickup location.
        ///
        ///    For example, this request:
        ///         ABC Movers
        ///         1500 East Main Avenue, Suite 201
        ///         Springfield, VA 22162
        ///
        ///    Returns this response:
        ///         ABC MOVERS
        ///         1500 E MAIN AVE STE 201
        ///    SPRINGFIELD, VA 22162-1010
        /// 6. Package pickup is available only for domestic addresses.
        /// </summary>
        /// <returns>The schedule.</returns>
        /// <param name="request">Request.</param>
        /// <param name="session">Session.</param>
        /// <typeparam name="T">A type that implements IPickup.</typeparam>
        public async static Task<ShippingApiResponse<T>> Schedule<T>(T request, ISession session = null) where T : IPickup, new()
        {
            var scheduleRequest = new JsonPickup<T>(request);
            return await WebMethod.Post<T, JsonPickup<T>>("/shippingservices/v1/pickups/schedule", scheduleRequest, session);
        }
        /// <summary>
        /// Cancels the pickup.
        /// </summary>
        /// <returns>The pickup.</returns>
        /// <param name="request">Request.</param>
        /// <param name="session">Session.</param>
        public async static Task<ShippingApiResponse<PickupCancelRequest>> CancelPickup(PickupCancelRequest request, ISession session = null)
        {
            request.Status = "Success";
            return await WebMethod.Post<PickupCancelRequest, PickupCancelRequest>("/v1/pickups/{PickupId}/cancel", request, session);
        }
    }

}
