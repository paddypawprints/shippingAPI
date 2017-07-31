using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.pb.shippingapi.model
{
    public class Merchant
    {
        [JsonProperty("fullName")]
        public string FullName {get;set;}

        [JsonProperty("email")]
        public string Email {get;set;}
        [JsonProperty("registeredDate")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTimeOffset RegisteredDate { get;set;}
        [JsonProperty("paymentAccountNumber")]
        public string PaymentAccountNumber {get;set;}
        [JsonProperty("enterpriseAccoubnt")]
        public string EnterpriseAccount {get;set;}
        [JsonProperty("subscriptionAccount")]
        public string SubscriptionAccount {get;set;}
        [JsonProperty("postalReportingNumber")]
        public string PostalReportingNumber {get;set;}
        [JsonProperty("merchantStatus")]
        public string MerchantStatus {get;set;}
        [JsonProperty("merchantStatusReason")]
        public string MerchantStatusReason {get;set;}
        [JsonProperty("deactivatedDate")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTimeOffset DeactivatedDate {get;set;}
        
    }
}
