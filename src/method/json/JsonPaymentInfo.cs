/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonPaymentInfo<T> : JsonWrapper<T>, IPaymentInfo where T : IPaymentInfo, new()
    {
        public JsonPaymentInfo() : base() { }
        public JsonPaymentInfo(T t) : base(t) { }

        [JsonProperty("paymentType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentType PaymentType
        {
            get => Wrapped.PaymentType;
            set { Wrapped.PaymentType = value; }
        }
        [JsonProperty("paymentMethod")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentMethod PaymentMethod
        {
            get => Wrapped.PaymentMethod;
            set { Wrapped.PaymentMethod = value; }
        }
        [JsonProperty("ppPaymentDetails")]
        public IPpPaymentDetails PpPaymentDetails
        {
            get => Wrapped.PpPaymentDetails;
            set { Wrapped.PpPaymentDetails = value; }
        }
        [JsonProperty("ccPaymentDetails")]
        public ICcPaymentDetails CcPaymentDetails
        {
            get => Wrapped.CcPaymentDetails;
            set { Wrapped.CcPaymentDetails = value; }
        }
    }
}
