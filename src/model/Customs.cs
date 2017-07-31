
using Newtonsoft.Json;
using System.Collections.Generic;

namespace com.pb.shippingapi.model
{
    public class Customs
    {
        [JsonProperty("customsInfo")]
        public CustomsInfo CustomsInfo { get;set;}
        [JsonProperty("customsItems")]
        public IEnumerable<CustomsItems> CustomsItems {get;set;}
    }
}