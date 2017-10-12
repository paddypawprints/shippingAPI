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

using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyBowes.Developer.ShippingApi.Json;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Method
{

    [JsonObject(MemberSerialization.OptIn)]
    public class CancelShipmentRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }
        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }
        [ShippingApiHeader("X-PB-TransactionId")]
        public string TransactionId {get;set;}
        [ShippingApiResource("shipments", AddId = true)]
        public string ShipmentToCancel {get;set;}
        [JsonProperty("carrier")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Carrier Carrier {get;set;}
        [JsonProperty("cancelInitiator")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CancelInitiator CancelInitiator {get;set;}
    }

   public class CancelShipmentResponse 
    {
        [JsonProperty("carrier")]
        public Carrier Carrier {get;set;}
        [JsonProperty("cancelInitiator")]
        public CancelInitiator CancelInitiator {get;set;}
        [JsonProperty("totalCarrierCharge")]
        public decimal TotalCarrierCharge {get;set;}
        [JsonProperty("parcelTrackingNumber")]
        public string ParcelTrackingNumber {get;set;}
        [JsonProperty("status")]
        public RefundStatus RefundStatus {get;set;}
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ReprintShipmentRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }
        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }
        [ShippingApiResource("shipments", AddId = true)]
        public string Shipment {get;set;}
    }

    public static class ShipmentsMethods
    {
        public async static Task<ShippingApiResponse<T>> CreateShipment<T>(T request, ISession session = null) where T:IShipment, new()
        {
            var wrapped = new JsonShipment<T>(request);
            return await WebMethod.Post< T, JsonShipment<T>>( "/shippingservices/v1/shipments", wrapped, session );
        }
        public async static Task<ShippingApiResponse<CancelShipmentResponse>> CancelShipment( CancelShipmentRequest request, ISession session = null)
        {
            return await WebMethod.DeleteWithBody<CancelShipmentResponse, CancelShipmentRequest>( "/shippingservices/v1", request, session );
        }
        public async static Task<ShippingApiResponse<T>>  ReprintShipment<T>(ReprintShipmentRequest request, ISession session = null) where T : IShipment, new()
        {
            return  await WebMethod.Get<T, ReprintShipmentRequest>( "/shippingservices/v1", request, session );
        }
    }
}