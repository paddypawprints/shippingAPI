using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonWeightRule<T> : JsonWrapper<T>, IWeightRule where T : IWeightRule, new()
    {
        public JsonWeightRule() : base() { }
        public JsonWeightRule(T t) : base(t) { }

        [JsonProperty("required")]
        public Boolean Required
        {
            get => Wrapped.Required;
            set { Wrapped.Required = value; }
        }
        [JsonProperty("unitOfWeight")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfWeight UnitOfWeight
        {
            get => Wrapped.UnitOfWeight;
            set { Wrapped.UnitOfWeight = value; }
        }
        [JsonProperty("minWeight")]
        public Decimal MinWeight
        {
            get => Wrapped.MinWeight;
            set { Wrapped.MinWeight = value; }
        }
        [JsonProperty("maxWeight")]
        public Decimal MaxWeight
        {
            get => Wrapped.MaxWeight;
            set { Wrapped.MaxWeight = value; }
        }
    }
}
