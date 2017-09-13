using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ITransaction
    {
        string TransactionId {get;set;}
        DateTimeOffset TransactionDateTime { get; set; }
        TransactionType TransactionType { get; set; }
        string DeveloperName { get; set; }
        string DeveloperId { get; set; }
        string DeveloperPostagePaymentMethod { get; set; }
        string DeveloperRatePlan { get; set; }
        decimal? DeveloperRateAmount { get; set; }
        decimal? DeveloperPostagePaymentAccountBalance { get; set; }
        string MerchantName { get; set; }
        string MerchantId { get; set; }
        string MerchantPostageAccountPaymentMethod { get; set; }
        string MerchantRatePlan { get; set; }
        decimal? MerchantRate { get; set; }
        decimal? ShipperPostagePaymentAccountBalance { get; set; }
        decimal? LabelFee { get; set; }
        string ParcelTrackingNumber { get; set; }
        decimal? WeightInOunces { get; set; }
        int? Zone { get; set; }
        decimal? PackageLengthInInches { get; set; }
        decimal? PackageWidthInInches { get; set; }
        decimal? PackageHeightInInches { get; set; }
        PackageTypeIndicator? PackageTypeIndicator { get; set; }
        USPSParcelType? PackageType { get; set; }
        string MailClass { get; set; }
        string InternationalCountryPriceGroup { get; set; }
        string OriginationAddress { get; set; }
        string OriginZip { get; set; }
        string DestinationAddress { get; set; }
        string DestinationZip { get; set; }
        string DestinationCountry { get; set; }
        decimal? PostageDepositAmount { get; set; }
        decimal? CreditCardFee { get; set; }
        string RefundStatus { get; set; }
        string RefundDenialReason { get; set; }
    }
}
