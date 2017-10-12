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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCarrierPickup<T> : JsonWrapper<T>, ICarrierPickup where T :  ICarrierPickup, new()
    {
        public JsonCarrierPickup() : base() { }
        public JsonCarrierPickup(T t) : base(t) { }

        [JsonProperty("pickupAddress")]
        public IAddress PickupAddress
        {
            get => Wrapped.PickupAddress;
            set { Wrapped.PickupAddress = value; }
        }
        [JsonProperty("carrier")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Carrier Carrier
        {
            get => Wrapped.Carrier;
            set { Wrapped.Carrier = value; }
        }
        [JsonProperty("pickupSummary")]
        public IEnumerable<IPickupCount> PickupSummary
        {
            get => Wrapped.PickupSummary;
            set { Wrapped.PickupSummary = value; }
        }
        [JsonProperty("required")]
        public string Required
        {
            get => Wrapped.Required;
            set { Wrapped.Required = value; }
        }
        [JsonProperty("packageLocation")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PackageLocation PackageLocation
        {
            get => Wrapped.PackageLocation;
            set { Wrapped.PackageLocation = value; }
        }
        [JsonProperty("specialInstructions")]
        public string SpecialInstructions
        {
            get => Wrapped.SpecialInstructions;
            set { Wrapped.SpecialInstructions = value; }
        }
        [JsonProperty("pickupDateTime")]
        public DateTimeOffset PickupDateTime
        {
            get => Wrapped.PickupDateTime;
            set { Wrapped.PickupDateTime = value; }
        }
        [JsonProperty("pickupConfirmationNumber")]
        public string PickupConfirmationNumber
        {
            get => Wrapped.PickupConfirmationNumber;
            set { Wrapped.PickupConfirmationNumber = value; }
        }
        [JsonProperty("pickupId")]
        public string PickupId
        {
            get => Wrapped.PickupId;
            set { Wrapped.PickupId = value; }
        }
    }
}
