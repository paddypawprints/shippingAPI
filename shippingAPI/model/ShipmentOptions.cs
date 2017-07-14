using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class ShipmentOptions
    {
        [JsonProperty("name")]
        public ShipmentOption ShipmentOption { get; set;}
        [JsonProperty("value")]
        public string Value { get;set;}

    }
}