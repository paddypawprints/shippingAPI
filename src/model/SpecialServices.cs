using Newtonsoft.Json;
using System.Collections.Generic;

namespace com.pb.shippingapi.model
{
    public class SpecialServices
    {
        [JsonProperty("specialServiceID")]
        public USPSSpecialServiceCodes SpecialServiceId { get; set;}
        [JsonProperty("inputParameters")]
        public IEnumerable<Parameter> InputParameters { get;set;}
       [JsonProperty("fee")]
        public decimal Fee { get; set;}

    }
}