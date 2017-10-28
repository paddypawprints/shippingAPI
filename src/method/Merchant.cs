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
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Method
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RenderRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }

        [ShippingApiHeaderAttribute("Accept-Language")]
        public string AcceptLanguage { get => "en-US"; }

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [JsonProperty("hostUri")]
        public string HostUri { get; set; }

        [JsonProperty("renderType")]
        public string RenderType { get; set; }

        [JsonProperty("returnUrl")]
        public string ReturnUrl { get; set; }

        [JsonProperty("styleSheet")]
        public string StyleSheet { get; set; }

        [JsonProperty("userInfo")]
        public IUserInfo UserInfo { get; set; }
    }

    public class Frame
    {
        public string renderType;
        public string render;
    }

    public class RenderResponse
    {
        public Frame purchasePower;
        public string accessToken;
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MerchantSignupRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        public string DeveloperId { get; set; }

        [JsonProperty("contact")]
        public IAddress Contact { get; set; }

        [JsonProperty("paymentInfo")]
        public IEnumerable<IPaymentInfo> PaymentInfo { get; set; }

        [JsonProperty("initialPostageBalance")]
        public decimal IntialPostageValue { get; set; }

        [JsonProperty("refillAmount")]
        public decimal RefillAmount { get; set; }

        [JsonProperty("thresholdAmount")]
        public decimal ThresholdAmount { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MerchantCredentialsRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        public string DeveloperId { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public StringBuilder Password { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MerchantRegisterRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        public string DeveloperId { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("addressLines", Order = 5)]
        public IEnumerable<string> AddressLines { get;set;}
        [JsonProperty("cityTown", Order = 6)]
        public string CityTown {get;set;}
        [JsonProperty("stateProvince", Order = 7)]
        public string StateProvince {get; set;}
        [JsonProperty("postalCode", Order = 8)]
        public string PostalCode {get;set;}
        [JsonProperty("countryCode", Order = 9)]
        public string CountryCode{ get;set;}
        [JsonProperty("company", Order = 0)]
        public string Company { get;set; }
        [JsonProperty("name", Order = 1)]
        public string Name{ get;set; }
        [JsonProperty("phone", Order = 2)]
        public string Phone{get;set; }
        [JsonProperty("email", Order = 3)]
        public string Email{ get;set; }
        [JsonProperty("residential", Order = 4)]
        public bool Residential { get;set;}
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MerchantAutoRefillRuleRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }
        public string DeveloperId { get; set; }
        public string ShipperId { get; set; }
        [JsonProperty("merchantID")]
        public string MerchantID { get; set; }
        [JsonProperty("threshold")]
        public decimal Threshold { get; set; }
        [JsonProperty("addAmount")]
        public decimal AddAmount { get; set; }
        [JsonProperty("enabled")]
        public Boolean Enabled { get; set; }

    }

    public class AccountBalanceRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        public string AccountNumber { get; set; }
    }

    public class AccountBalanceResponse
    {
        [JsonProperty("accountNumber")]
        string AccountNumber { get; set; }
        [JsonProperty("balance")]
        decimal Balance { get; set; }
        [JsonProperty("currencyCode")]
        string CurrencyCode { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MerchantDeactivateRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        public string DeveloperId { get; set; }

        public string AccountId { get; set; }

        [JsonProperty("reason")]
        string Reason { get; set; }

    }

    public static class MerchantMethods
    {
        public async static Task<ShippingApiResponse<RenderResponse>> Render<RenderResponse>(RenderRequest request, ISession session = null) 
        {
            return await WebMethod.Post<RenderResponse, RenderRequest>("/shippingservices/v1/payment/render", request, session);
        }
        public async static Task<ShippingApiResponse<IMerchant>> Signup<IMerchant>(MerchantSignupRequest request, ISession session = null)
        {
            return await WebMethod.Post<IMerchant, MerchantSignupRequest>("/shippingservices/v1/developers/{DeveloperId}/merchants/signup", request, session);
        }

        public async static Task<ShippingApiResponse<IMerchant>> Credentials<IMerchant>(MerchantCredentialsRequest request, ISession session = null)
        {
            return await WebMethod.Post<IMerchant, MerchantCredentialsRequest>("/shippingservices/v1/developers/{DeveloperId}/merchants/credentials", request, session);
        }
        public async static Task<ShippingApiResponse<IMerchant>> Register<IMerchant>(MerchantRegisterRequest request, ISession session = null)
        {
            return await WebMethod.Post<IMerchant, MerchantRegisterRequest>("/shippingservices/v1//developers/{DeveloperId}/merchants/registration" , request, session);
        }

        public async static Task<ShippingApiResponse<IAutoRefill>> AutoRefillRule<IAutoRefill>(MerchantAutoRefillRuleRequest request, ISession session = null)
        {
            return await WebMethod.Get<IAutoRefill, MerchantAutoRefillRuleRequest>("/shippingservices/v1/developers/{DeveloperId}/merchants/{AccountId}/autorefillrule", request, session);
        }

        public async static Task<ShippingApiResponse<IAutoRefill>> UpdateAutoRefill<IAutoRefill>(MerchantAutoRefillRuleRequest request, ISession session = null)
        {
            return await WebMethod.Post<IAutoRefill, MerchantAutoRefillRuleRequest>("/shippingservices/v1/developers/{DeveloperId}/merchants/{ShipperId}/autorefillrule", request, session);
        }
        public async static Task<ShippingApiResponse<AccountBalanceResponse>> UpdateAutoRefill<AccountBalanceResponse>(AccountBalanceRequest request, ISession session = null)
        {
            return await WebMethod.Get<AccountBalanceResponse, AccountBalanceRequest>("/shippingservices/v1/ledger/accounts/{AccountId}/balance ", request, session);
        }
        public async static Task<ShippingApiResponse<IMerchant>> MerchantDeactivateAccount<IMerchant>(MerchantDeactivateRequest request, ISession session = null)
        {
            return await WebMethod.Get<IMerchant, MerchantDeactivateRequest>("/shippingservices/v2/developers/{DeveloperId}/accounts/{ShipperId}/deactivate ", request, session);
        }
    }
}
