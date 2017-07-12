
using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class Country
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set;}
        [JsonProperty("countryName")]
        public string CountryName { get; set;}
    }
}