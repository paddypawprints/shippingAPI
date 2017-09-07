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
