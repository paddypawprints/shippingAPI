using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.pb.shippingapi.model
{
    public class ShipmentOptions
    {
        [JsonProperty("name")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ShipmentOption ShipmentOption { get; set;}
        [JsonProperty("value")]
        public string Value { get;set;}

    }
}