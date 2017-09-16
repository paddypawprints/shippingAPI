using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonSpecialServicesRule<T> : JsonWrapper<T>, ISpecialServicesRule where T : ISpecialServicesRule, new()
    {
        public JsonSpecialServicesRule() : base() { }
        public JsonSpecialServicesRule(T t) : base(t) { }

        [JsonProperty("specialServiceId")]
        public string SpecialServiceId
        {
            get => Wrapped.SpecialServiceId;
            set { Wrapped.SpecialServiceId = value; }
        }
        [JsonProperty("brandedName")]
        public string BrandedName
        {
            get => Wrapped.BrandedName;
            set { Wrapped.BrandedName = value; }
        }
        [JsonProperty("categoryId")]
        public string CategoryId
        {
            get => Wrapped.CategoryId;
            set { Wrapped.CategoryId = value; }
        }
        [JsonProperty("categoryName")]
        public string CategoryName
        {
            get => Wrapped.CategoryName;
            set { Wrapped.CategoryName = value; }
        }
        [JsonProperty("trackable")]
        public Boolean Trackable
        {
            get => Wrapped.Trackable;
            set { Wrapped.Trackable = value; }
        }
        [JsonProperty("inputParameterRules")]
        public IEnumerable<IServicesParameterRule> InputParameterRules
        {
            get => Wrapped.InputParameterRules;
            set { Wrapped.InputParameterRules = value; }
        }
        [JsonProperty("prerequisiteRules")]
        public IEnumerable<IServicesPrerequisiteRule> PrerequisiteRules
        {
            get => Wrapped.PrerequisiteRules;
            set { Wrapped.PrerequisiteRules = value; }
        }
        [JsonProperty("incompatibleSpecialServices")]
        public IEnumerable<SpecialServiceCodes> IncompatibleSpecialServices
        {
            get => Wrapped.IncompatibleSpecialServices;
            set { Wrapped.IncompatibleSpecialServices = value; }
        }
    }
}
