using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class Parcel
    {
        [JsonProperty("dimension")]
        public ParcelDimension Dimension {get;set;}
        [JsonProperty("weight")]
        public ParcelWeight Weight {get;set;}
        [JsonProperty("valueOfGoods")]
        public decimal ValueOfGoods { get;set;}
        [JsonProperty("currencyCode")]
        public string CurrencyCode {get;set;}
        
    }
}