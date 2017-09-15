using System;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Merchant : IMerchant
    {
        public string FullName {get;set;}
        public string Email {get;set;}
        public DateTimeOffset RegisteredDate { get;set;}
        public string PaymentAccountNumber {get;set;}
        public string EnterpriseAccount {get;set;}
        public string SubscriptionAccount {get;set;}
        public string PostalReportingNumber {get;set;}
        public string MerchantStatus {get;set;}
        public string MerchantStatusReason {get;set;}
        public DateTimeOffset DeactivatedDate {get;set;}       
    }

}
