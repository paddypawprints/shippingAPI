
using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class Customs
    {
        [JsonProperty("customsInfo")]
        public CustomsInfo CustomsInfo { get;set;}
        [JsonProperty("customsItems")]
        public CustomsItems[] CustomsItems {get;set;}
    }
}