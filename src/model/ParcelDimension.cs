
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.pb.shippingapi.model
{
    public class ParcelDimension
    {
        [JsonProperty("length")]
        public decimal Length { get; set;}
        [JsonProperty("height")]
        public decimal Height { get; set;}
        [JsonProperty("width")]
        public decimal Width { get;set;}
        [JsonProperty("unitOfMeasurement")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfDimension UnitOfMeasurement {get;set;}
        [JsonProperty("irregularParcelGirth")]
        public string CountryCode {get;set;}

    }
}