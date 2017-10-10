using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WeightRule
    {
        [JsonProperty("required")]
        public Boolean Required{get; set;}
        [JsonProperty("unitOfWeight")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfWeight UnitOfWeight{get; set;}
        [JsonProperty("minWeight")]
        public Decimal MinWeight{get; set;}
        [JsonProperty("maxWeight")]
        public Decimal MaxWeight{get; set;}
    }
}
