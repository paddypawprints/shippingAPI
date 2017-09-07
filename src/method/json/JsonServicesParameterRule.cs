using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonServicesParameterRule<T> : JsonWrapper<T>, IServicesParameterRule where T : IServicesParameterRule, new()
    {
        public JsonServicesParameterRule() : base() { }
        public JsonServicesParameterRule(T t) : base(t) { }

        [JsonProperty("name")]
        public string Name
        {
            get => Wrapped.Name;
            set { Wrapped.Name = value; }
        }
        [JsonProperty("brandedName")]
        public string BrandedName
        {
            get => Wrapped.BrandedName;
            set { Wrapped.BrandedName = value; }
        }
        [JsonProperty("required")]
        public Boolean Required
        {
            get => Wrapped.Required;
            set { Wrapped.Required = value; }
        }
        [JsonProperty("minValue")]
        public Decimal MinValue
        {
            get => Wrapped.MinValue;
            set { Wrapped.MinValue = value; }
        }
        [JsonProperty("maxValue")]
        public Decimal MaxValue
        {
            get => Wrapped.MaxValue;
            set { Wrapped.MaxValue = value; }
        }
        [JsonProperty("freeValue")]
        public Decimal FreeValue
        {
            get => Wrapped.FreeValue;
            set { Wrapped.FreeValue = value; }
        }
        [JsonProperty("format")]
        public string Format
        {
            get => Wrapped.Format;
            set { Wrapped.Format = value; }
        }
        [JsonProperty("description")]
        public string Description
        {
            get => Wrapped.Description;
            set { Wrapped.Description = value; }
        }
    }
}
