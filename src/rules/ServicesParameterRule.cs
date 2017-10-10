using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ServicesParameterRule
    {
        [JsonProperty("name")]
        public string Name{get; set;}
        [JsonProperty("brandedName")]
        public string BrandedName{get; set;}
        [JsonProperty("required")]
        public Boolean Required{get; set;}
        [JsonProperty("minValue")]
        public Decimal MinValue{get; set;}
        [JsonProperty("maxValue")]
        public Decimal MaxValue{get; set;}
        [JsonProperty("freeValue")]
        public Decimal FreeValue{get; set;}
        [JsonProperty("format")]
        public string Format{get; set;}
        [JsonProperty("description")]
        public string Description{get; set;}
    }
}
