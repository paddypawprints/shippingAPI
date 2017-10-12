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

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonMerchant<T> : JsonWrapper<T>, IMerchant where T:IMerchant,new()
    {
        public JsonMerchant() : base() { }

        public JsonMerchant(T t) : base(t) { }

        [JsonProperty("fullName")]
        public string FullName
        {
            get => Wrapped.FullName;
            set { Wrapped.FullName = value; }
        }
        [JsonProperty("email")]
        public string Email
        {
            get => Wrapped.Email;
            set { Wrapped.Email = value; }
        }
        [JsonProperty("registeredDate")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTimeOffset RegisteredDate
        {
            get => Wrapped.RegisteredDate;
            set { Wrapped.RegisteredDate = value; }
        }
        [JsonProperty("paymentAccountNumber")]
        public string PaymentAccountNumber
        {
            get => Wrapped.PaymentAccountNumber;
            set { Wrapped.PaymentAccountNumber = value; }
        }
        [JsonProperty("enterpriseAccount")]
        public string EnterpriseAccount
        {
            get => Wrapped.EnterpriseAccount;
            set { Wrapped.EnterpriseAccount = value; }
        }
        [JsonProperty("subscriptionAccount")]
        public string SubscriptionAccount
        {
            get => Wrapped.SubscriptionAccount;
            set { Wrapped.SubscriptionAccount = value; }
        }
        [JsonProperty("postalReportingNumber")]
        public string PostalReportingNumber
        {
            get => Wrapped.PostalReportingNumber;
            set { Wrapped.PostalReportingNumber = value; }
        }
        [JsonProperty("merchantStatus")]
        public string MerchantStatus
        {
            get => Wrapped.MerchantStatus;
            set { Wrapped.MerchantStatus = value; }
        }
        [JsonProperty("merchantStatusReason")]
        public string MerchantStatusReason
        {
            get => Wrapped.MerchantStatusReason;
            set { Wrapped.MerchantStatusReason = value; }
        }
        [JsonProperty("deactivatedDate")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTimeOffset DeactivatedDate
        {
            get => Wrapped.DeactivatedDate;
            set { Wrapped.DeactivatedDate = value; }
        }
    }

}
