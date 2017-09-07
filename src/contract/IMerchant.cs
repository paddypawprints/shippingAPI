using System;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IMerchant
    {
        string FullName { get; set; }
        string Email { get; set; }
        DateTimeOffset RegisteredDate { get; set; }
        string PaymentAccountNumber { get; set; }
        string EnterpriseAccount { get; set; }
        string SubscriptionAccount { get; set; }
        string PostalReportingNumber { get; set; }
        string MerchantStatus { get; set; }
        string MerchantStatusReason { get; set; }
        DateTimeOffset DeactivatedDate { get; set; }

    }

}
