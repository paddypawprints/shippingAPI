using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace com.pb.shippingapi.model
{
    public class SpecialServices
    {
        [JsonProperty("specialServiceId")]
        [JsonConverter(typeof(StringEnumConverter))]
        public USPSSpecialServiceCodes SpecialServiceId { get; set;}
        [JsonProperty("inputParameters")]
        public IEnumerable<Parameter> InputParameters { get;set;}
       [JsonProperty("fee")]
        public decimal Fee { get; set;}

    }
}