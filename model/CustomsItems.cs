
using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class CustomsItems
    {
        [JsonProperty("description")]
        public string Description { get; set;}
        [JsonProperty("quantity")]
        public int Quantity { get; set;}
        [JsonProperty("unitPrice")]
        public decimal UnitPrice { get;set;}
        [JsonProperty("unitWeight")]
        public ParcelWeight UnitWeight {get;set;}
        [JsonProperty("hSTariffCode")]
        public string HSTariffCode {get;set;}
        [JsonProperty("originCountryCode")]
        public string OriginCountryCode {get;set;}
        
    }
}