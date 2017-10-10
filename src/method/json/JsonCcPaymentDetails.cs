/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/


using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCcPaymentDetails<T> : JsonWrapper<T>, ICcPaymentDetails where T : ICcPaymentDetails, new()
    {
        public JsonCcPaymentDetails() : base() { }
        public JsonCcPaymentDetails(T t) : base(t) { }

        [JsonProperty("ccType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CreditCardType CcType
        {
            get => Wrapped.CcType;
            set { Wrapped.CcType = value; }
        }
        [JsonProperty("ccTokenNumber")]
        public string CcTokenNumber
        {
            get => Wrapped.CcTokenNumber;
            set { Wrapped.CcTokenNumber = value; }
        }
        [JsonProperty("ccExpirationDate")]
        public string CcExpirationDate
        {
            get => Wrapped.CcExpirationDate;
            set { Wrapped.CcExpirationDate = value; }
        }
        [JsonProperty("cccvvNumber")]
        public string CccvvNumber
        {
            get => Wrapped.CccvvNumber;
            set { Wrapped.CccvvNumber = value; }
        }
        [JsonProperty("ccAddress")]
        public IAddress CcAddress
        {
            get => Wrapped.CcAddress;
            set { Wrapped.CcAddress = value; }
        }
    }
}
