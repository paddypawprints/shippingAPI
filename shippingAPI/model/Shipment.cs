using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class Shipment
    {
        [JsonProperty("fromAddress")]
        public Address FromAddress { get; set;}
        [JsonProperty("toAddress")]
        public Address ToAddress { get;set;}
        [JsonProperty("altReturnAddress")]
        public Address AltReturnAddress { get; set;}
        [JsonProperty("parcel")]
        public Parcel Parcel { get; set;}
        [JsonProperty("rates")]
        public Rates Rates { get; set;}
        [JsonProperty("documents")]
        public Document[] Documents { get; set;}
        [JsonProperty("shipmentOptions")]
        public ShipmentOptions[] ShipmentOptions { get; set;}
        [JsonProperty("customs")]
        public Customs Customs {get;set;}
        [JsonProperty("shipmentId")]
        public string ShipmentId { get; set;}
        [JsonProperty("parcelTrackingNumber")]
        public string ParcelTrackingNumber {get;set;}
        
    }
}