using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class Parameter
    {
        [JsonProperty("name")]
        public string Name {get;set;}
        [JsonProperty("value")]
        public string Value {get;set;}
    }
}