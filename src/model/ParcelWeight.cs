
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.pb.shippingapi.model
{
    public class ParcelWeight
    {
        [JsonProperty("weight")]
        public decimal Weight { get; set;}
        [JsonProperty("unitOfMeasurement")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfWeight UnitOfMeasurement { get; set;}

    }
}