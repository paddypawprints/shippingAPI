using System;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Merchant : IMerchant
    {
        virtual public string FullName {get;set;}
        virtual public string Email {get;set;}
        virtual public DateTimeOffset RegisteredDate { get;set;}
        virtual public string PaymentAccountNumber {get;set;}
        virtual public string EnterpriseAccount {get;set;}
        virtual public string SubscriptionAccount {get;set;}
        virtual public string PostalReportingNumber {get;set;}
        virtual public string MerchantStatus {get;set;}
        virtual public string MerchantStatusReason {get;set;}
        virtual public DateTimeOffset DeactivatedDate {get;set;}       
    }

}
