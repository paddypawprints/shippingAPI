
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonTransaction<T> : JsonWrapper<T>, ITransaction where T : ITransaction, new()
    {
        public JsonTransaction() : base() { }
        public JsonTransaction(T t) : base(t) { }

        [JsonProperty("transactionId")]
        public string TransactionId
        {
            get => Wrapped.TransactionId;
            set { Wrapped.TransactionId = value; }
        }
        [JsonProperty("transactionDateTime")]
        public DateTimeOffset TransactionDateTime
        {
            get => Wrapped.TransactionDateTime;
            set { Wrapped.TransactionDateTime = value; }
        }
        [JsonProperty("transactionType")]
        [JsonConverter(typeof(TransactionTypeConverter))]
        public TransactionType TransactionType
        {
            get => Wrapped.TransactionType;
            set { Wrapped.TransactionType = value; }
        }
        [JsonProperty("developerName")]
        public string DeveloperName
        {
            get => Wrapped.DeveloperName;
            set { Wrapped.DeveloperName = value; }
        }
        [JsonProperty("developerId")]
        public string DeveloperId
        {
            get => Wrapped.DeveloperId;
            set { Wrapped.DeveloperId = value; }
        }
        [JsonProperty("developerPostagePaymentMethod")]
        public string DeveloperPostagePaymentMethod
        {
            get => Wrapped.DeveloperPostagePaymentMethod;
            set { Wrapped.DeveloperPostagePaymentMethod = value; }
        }
        [JsonProperty("developerRatePlan")]
        public string DeveloperRatePlan
        {
            get => Wrapped.DeveloperRatePlan;
            set { Wrapped.DeveloperRatePlan = value; }
        }
        [JsonProperty("developerRateAmount")]
        public Decimal? DeveloperRateAmount
        {
            get => Wrapped.DeveloperRateAmount;
            set { Wrapped.DeveloperRateAmount = value; }
        }
        [JsonProperty("developerPostagePaymentAccountBalance")]
        public Decimal? DeveloperPostagePaymentAccountBalance
        {
            get => Wrapped.DeveloperPostagePaymentAccountBalance;
            set { Wrapped.DeveloperPostagePaymentAccountBalance = value; }
        }
        [JsonProperty("merchantName")]
        public string MerchantName
        {
            get => Wrapped.MerchantName;
            set { Wrapped.MerchantName = value; }
        }
        [JsonProperty("merchantId")]
        public string MerchantId
        {
            get => Wrapped.MerchantId;
            set { Wrapped.MerchantId = value; }
        }
        [JsonProperty("merchantPostageAccountPaymentMethod")]
        public string MerchantPostageAccountPaymentMethod
        {
            get => Wrapped.MerchantPostageAccountPaymentMethod;
            set { Wrapped.MerchantPostageAccountPaymentMethod = value; }
        }
        [JsonProperty("merchantRatePlan")]
        public string MerchantRatePlan
        {
            get => Wrapped.MerchantRatePlan;
            set { Wrapped.MerchantRatePlan = value; }
        }
        [JsonProperty("merchantRate")]
        public Decimal? MerchantRate
        {
            get => Wrapped.MerchantRate;
            set { Wrapped.MerchantRate = value; }
        }
        [JsonProperty("shipperPostagePaymentAccountBalance")]
        public Decimal? ShipperPostagePaymentAccountBalance
        {
            get => Wrapped.ShipperPostagePaymentAccountBalance;
            set { Wrapped.ShipperPostagePaymentAccountBalance = value; }
        }
        [JsonProperty("labelFee")]
        public Decimal? LabelFee
        {
            get => Wrapped.LabelFee;
            set { Wrapped.LabelFee = value; }
        }
        [JsonProperty("parcelTrackingNumber")]
        public string ParcelTrackingNumber
        {
            get => Wrapped.ParcelTrackingNumber;
            set { Wrapped.ParcelTrackingNumber = value; }
        }
        [JsonProperty("weightInOunces")]
        public Decimal? WeightInOunces
        {
            get => Wrapped.WeightInOunces;
            set { Wrapped.WeightInOunces = value; }
        }
        [JsonProperty("zone")]
        public int? Zone
        {
            get => Wrapped.Zone;
            set { Wrapped.Zone = value; }
        }
        [JsonProperty("packageLengthInInches")]
        public Decimal? PackageLengthInInches
        {
            get => Wrapped.PackageLengthInInches;
            set { Wrapped.PackageLengthInInches = value; }
        }
        [JsonProperty("packageWidthInInches")]
        public Decimal? PackageWidthInInches
        {
            get => Wrapped.PackageWidthInInches;
            set { Wrapped.PackageWidthInInches = value; }
        }
        [JsonProperty("packageHeightInInches")]
        public Decimal? PackageHeightInInches
        {
            get => Wrapped.PackageHeightInInches;
            set { Wrapped.PackageHeightInInches = value; }
        }
        [JsonProperty("packageTypeIndicator")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PackageTypeIndicator? PackageTypeIndicator
        {
            get => Wrapped.PackageTypeIndicator;
            set { Wrapped.PackageTypeIndicator = value; }
        }
        [JsonProperty("packageType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ParcelType? PackageType
        {
            get => Wrapped.PackageType;
            set { Wrapped.PackageType = value; }
        }
        [JsonProperty("mailClass")]
        public string MailClass
        {
            get => Wrapped.MailClass;
            set { Wrapped.MailClass = value; }
        }
        [JsonProperty("internationalCountryPriceGroup")]
        public string InternationalCountryPriceGroup
        {
            get => Wrapped.InternationalCountryPriceGroup;
            set { Wrapped.InternationalCountryPriceGroup = value; }
        }
        [JsonProperty("originationAddress")]
        public string OriginationAddress
        {
            get => Wrapped.OriginationAddress;
            set { Wrapped.OriginationAddress = value; }
        }
        [JsonProperty("originZip")]
        public string OriginZip
        {
            get => Wrapped.OriginZip;
            set { Wrapped.OriginZip = value; }
        }
        [JsonProperty("destinationAddress")]
        public string DestinationAddress
        {
            get => Wrapped.DestinationAddress;
            set { Wrapped.DestinationAddress = value; }
        }
        [JsonProperty("destinationZip")]
        public string DestinationZip
        {
            get => Wrapped.DestinationZip;
            set { Wrapped.DestinationZip = value; }
        }
        [JsonProperty("destinationCountry")]
        public string DestinationCountry
        {
            get => Wrapped.DestinationCountry;
            set { Wrapped.DestinationCountry = value; }
        }
        [JsonProperty("postageDepositAmount")]
        public Decimal? PostageDepositAmount
        {
            get => Wrapped.PostageDepositAmount;
            set { Wrapped.PostageDepositAmount = value; }
        }
        [JsonProperty("creditCardFee")]
        public Decimal? CreditCardFee
        {
            get => Wrapped.CreditCardFee;
            set { Wrapped.CreditCardFee = value; }
        }
        [JsonProperty("refundStatus")]
        public string RefundStatus
        {
            get => Wrapped.RefundStatus;
            set { Wrapped.RefundStatus = value; }
        }
        [JsonProperty("refundDenialReason")]
        public string RefundDenialReason
        {
            get => Wrapped.RefundDenialReason;
            set { Wrapped.RefundDenialReason = value; }
        }
    }
}