using System;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Transaction : ITransaction
    {
        public string TransactionId { get; set; }
        public DateTimeOffset TransactionDateTime { get; set; }
        public TransactionType TransactionType { get; set; }
        public string DeveloperName { get; set; }
        public string DeveloperId { get; set; }
        public string DeveloperPostagePaymentMethod { get; set; }
        public string DeveloperRatePlan { get; set; }
        public Decimal? DeveloperRateAmount { get; set; }
        public Decimal? DeveloperPostagePaymentAccountBalance { get; set; }
        public string MerchantName { get; set; }
        public string MerchantId { get; set; }
        public string MerchantPostageAccountPaymentMethod { get; set; }
        public string MerchantRatePlan { get; set; }
        public Decimal? MerchantRate { get; set; }
        public Decimal? ShipperPostagePaymentAccountBalance { get; set; }
        public Decimal? LabelFee { get; set; }
        public string ParcelTrackingNumber { get; set; }
        public Decimal? WeightInOunces { get; set; }
        public int? Zone { get; set; }
        public Decimal? PackageLengthInInches { get; set; }
        public Decimal? PackageWidthInInches { get; set; }
        public Decimal? PackageHeightInInches { get; set; }
        public PackageTypeIndicator? PackageTypeIndicator { get; set; }
        public USPSParcelType? PackageType { get; set; }
        public string MailClass { get; set; }
        public string InternationalCountryPriceGroup { get; set; }
        public string OriginationAddress { get; set; }
        public string OriginZip { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationZip { get; set; }
        public string DestinationCountry { get; set; }
        public Decimal? PostageDepositAmount { get; set; }
        public Decimal? CreditCardFee { get; set; }
        public string RefundStatus { get; set; }
        public string RefundDenialReason { get; set; }
    }
}
