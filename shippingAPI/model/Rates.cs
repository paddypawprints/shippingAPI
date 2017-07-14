using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class Rates
    {
        [JsonProperty("carrier")]
        public Carrier Carrier { get; set;}
        [JsonProperty("serviceId")]
        public USPSServices serviceId { get;set;}
        [JsonProperty("parcelType")]
        public USPSParcelType ParcelType { get; set;}
        [JsonProperty("specialServices")]
        public SpecialServices[] specialServices { get; set;}
        [JsonProperty("inductionPostalCode")]
        public string InductionPostalCode { get; set;}
        [JsonProperty("dimensionalWeight")]
        public ParcelWeight DimensionalWeight { get; set;}
        [JsonProperty("baseCharge")]
        public decimal BaseCharge { get; set;}
        [JsonProperty("totalCarrierCharge")]
        public decimal TotalCarrierCharge { get; set;}
        [JsonProperty("alternateBaseCharge")]
        public decimal AlternateBaseCharge { get; set;}
        [JsonProperty("alternateTotalCharge")]
        public decimal AlternateTotalCharge { get; set;}
        [JsonProperty("deliveryCommitment")]
        public DeliveryCommitment DeliveryCommitment { get; set;}
        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set;}
        [JsonProperty("destinationZone")]
        public int DestinationZone { get; set;}

    }
}