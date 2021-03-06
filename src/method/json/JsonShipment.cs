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
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonShipment<T> : JsonWrapper<T>, IShipment, IShippingApiRequest where T : IShipment, new()
    {
        public JsonShipment() : base() { }

        public JsonShipment(T t) : base(t) { }

        public string RecordingSuffix => "";
        public string RecordingFullPath(string resource, ISession session)
        {
            return ShippingApiRequest.RecordingFullPath(this, resource, session);
        }
        public string GetUri(string baseUrl)
        {
            return baseUrl;
        }

        public IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        public void SerializeBody(StreamWriter writer, ISession session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }

        [ShippingApiHeader("x-pb-transactionId", true)]
        public string TransactionId
        {
            get => Wrapped.TransactionId;
            set { Wrapped.TransactionId = value; }
        }

        public string ContentType { get => "application/json"; }

        [ShippingApiHeader("Bearer", true)]
        public StringBuilder Authorization { get; set; }

        [ShippingApiHeader("minimaladdressvalidation", true)]
        public string MinimalAddressValidation
        {
            get => Wrapped.MinimalAddressValidation;
            set { Wrapped.MinimalAddressValidation = value; }
        }


        [ShippingApiHeader("x-pb-shipper-rate-plan", true)]
        public string ShipperRatePlan
        {
            get => Wrapped.ShipperRatePlan;
            set { Wrapped.ShipperRatePlan = value; }
        }

        [JsonProperty("fromAddress")]
        public IAddress FromAddress
        {
            get => Wrapped.FromAddress;
            set { Wrapped.FromAddress = value; }
        }
        [JsonProperty("toAddress")]
        public IAddress ToAddress
        {
            get => Wrapped.ToAddress;
            set { Wrapped.ToAddress = value; }
        }
        [JsonProperty("altReturnAddress")]
        public IAddress AltReturnAddress
        {
            get => Wrapped.AltReturnAddress;
            set { Wrapped.AltReturnAddress = value; }
        }
        [JsonProperty("parcel")]
        public IParcel Parcel
        {
            get=>Wrapped.Parcel;
            set { Wrapped.Parcel = value; }
        }
        [JsonProperty("rates")]
        public IEnumerable<IRates> Rates
        {
            get => Wrapped.Rates;
            set { Wrapped.Rates = value; }
        }
        public IRates AddRates(IRates r)
        {
            return Wrapped.AddRates(r);
        }
        [JsonProperty("documents")]
        public IEnumerable<IDocument> Documents
        {
            get => Wrapped.Documents;
            set { Wrapped.Documents = value; }
        }
        public IDocument AddDocument(IDocument d)
        {
            return Wrapped.AddDocument(d);
        }

        [JsonProperty("shipmentOptions")]
        public IEnumerable<IShipmentOptions> ShipmentOptions
        {
            get => Wrapped.ShipmentOptions;
            set { Wrapped.ShipmentOptions = value; }
        }
        public IShipmentOptions AddShipmentOptions(IShipmentOptions s)
        {
            return Wrapped.AddShipmentOptions(s);
        }
        [JsonProperty("customs")]
        public ICustoms Customs
        {
            get => Wrapped.Customs;
            set { Wrapped.Customs = value; }
        }
        public bool ShouldSerializeShipmentType() => ShipmentType == ShipmentType.RETURN;
        [JsonProperty("shipmentType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ShipmentType ShipmentType
        {
            get => Wrapped.ShipmentType;
            set { Wrapped.ShipmentType = value; }
        }
        [JsonProperty("shipmentId")]
        public string ShipmentId
        { 
            get => Wrapped.ShipmentId;
            set { Wrapped.ShipmentId = value; }
        }
        [JsonProperty("parcelTrackingNumber")]
        public string ParcelTrackingNumber
        {
            get => Wrapped.ParcelTrackingNumber;
            set { Wrapped.ParcelTrackingNumber = value; }
        }
    }
}