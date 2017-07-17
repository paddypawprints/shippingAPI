
using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class ParcelWeight
    {
        [JsonProperty("weight")]
        public decimal Weight { get; set;}
        [JsonProperty("unitOfMeasurement")]
        public UnitOfWeight UnitOfMeasurement { get; set;}

    }
}