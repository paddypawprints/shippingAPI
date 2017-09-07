using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonServicesPrerequisiteRule<T> : JsonWrapper<T>, IServicesPrerequisiteRule where T : IServicesPrerequisiteRule, new()
    {
        public JsonServicesPrerequisiteRule() : base() { }
        public JsonServicesPrerequisiteRule(T t) : base(t) { }

        [JsonProperty("specialServiceId")]
        public string SpecialServiceId
        {
            get => Wrapped.SpecialServiceId;
            set { Wrapped.SpecialServiceId = value; }
        }
        [JsonProperty("minInputValue")]
        public string MinInputValue
        {
            get => Wrapped.MinInputValue;
            set { Wrapped.MinInputValue = value; }
        }
    }
}
