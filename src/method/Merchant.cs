﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;


namespace PitneyBowes.Developer.ShippingApi
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RenderRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }

        [ShippingAPIHeader("Accept-Language")]
        public string AcceptLanguage { get => "en-US"; }

        [ShippingAPIHeader("Bearer")]
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

        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingAPIResource("developers", AddId = true, PathSuffix = "/merchants/signup")]
        public string DeveloperId { get; set; }

        [JsonProperty("contact")]
        IAddress Contact { get; set; }

        [JsonProperty("paymentInfo")]
        IEnumerable<IPaymentInfo> PaymentInfo { get; set; }

        [JsonProperty("initialPostageBalance")]
        decimal IntialPostageValue { get; set; }

        [JsonProperty("refillAmount")]
        decimal RefillAmount { get; set; }

        [JsonProperty("thresholdAmount")]
        decimal ThresholdAmount { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MerchantCredentialsRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingAPIResource("developers", AddId = true, PathSuffix = "/merchants/credentials")]
        public string DeveloperId { get; set; }

        [JsonProperty("username")]
        string UserName { get; set; }

        [JsonProperty("password")]
        string Password { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MerchantRegisterRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingAPIResource("developers", AddId = true, PathSuffix = "/merchants/register")]
        public string DeveloperId { get; set; }

        [JsonProperty("username")]
        string UserName { get; set; }

        [JsonProperty("password")]
        string Password { get; set; }

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

        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }
        [ShippingAPIResource("developers", AddId = true)]
        public string DeveloperId { get; set; }
        [ShippingAPIResource("merchants", AddId = true, PathSuffix = "/autorefillrule")]
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

        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingAPIResource("accounts", AddId = true, PathSuffix = "/balance")]
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

        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingAPIResource("developers", AddId = true)]
        public string DeveloperId { get; set; }

        [ShippingAPIResource("accounts", AddId = true, PathSuffix = "/deactivate")]
        public string AccountId { get; set; }

        [JsonProperty("reason")]
        string Reason { get; set; }

    }

    public static class MerchantMethods
    {
        public async static Task<ShippingAPIResponse<RenderResponse>> Render<RenderResponse>(RenderRequest request, ShippingApi.Session session = null) 
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<RenderResponse, RenderRequest>("/shippingservices/v1/payment/render", request, session);
        }
        public async static Task<ShippingAPIResponse<IMerchant>> Signup<IMerchant>(MerchantSignupRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<IMerchant, MerchantSignupRequest>("/shippingservices/v1", request, session);
        }

        public async static Task<ShippingAPIResponse<IMerchant>> Credentials<IMerchant>(MerchantCredentialsRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<IMerchant, MerchantCredentialsRequest>("/shippingservices/v1", request, session);
        }
        public async static Task<ShippingAPIResponse<IMerchant>> Register<IMerchant>(MerchantRegisterRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<IMerchant, MerchantRegisterRequest>("/shippingservices/v1/" , request, session);
        }

        public async static Task<ShippingAPIResponse<IAutoRefill>> AutoRefillRule<IAutoRefill>(MerchantAutoRefillRuleRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<IAutoRefill, MerchantAutoRefillRuleRequest>("/shippingservices/v1/", request, session);
        }

        public async static Task<ShippingAPIResponse<IAutoRefill>> UpdateAutoRefill<IAutoRefill>(MerchantAutoRefillRuleRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<IAutoRefill, MerchantAutoRefillRuleRequest>("/shippingservices/v1/", request, session);
        }
        public async static Task<ShippingAPIResponse<AccountBalanceResponse>> UpdateAutoRefill<AccountBalanceResponse>(AccountBalanceRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<AccountBalanceResponse, AccountBalanceRequest>("/shippingservices/v1/ledger", request, session);
        }
        public async static Task<ShippingAPIResponse<IMerchant>> MerchantDeactivateAccount<IMerchant>(MerchantDeactivateRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<IMerchant, MerchantDeactivateRequest>("/shippingservices/v1/ledger", request, session);
        }
    }
}
