using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonParcelTypeRule<T> : JsonWrapper<T>, IParcelTypeRule where T : IParcelTypeRule, new()
    {
        public JsonParcelTypeRule() : base() { }
        public JsonParcelTypeRule(T t) : base(t) { }

        [JsonProperty("parcelType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public USPSParcelType ParcelType
        {
            get => Wrapped.ParcelType;
            set { Wrapped.ParcelType = value; }
        }
        [JsonProperty("brandedName")]
        public string BrandedName
        {
            get => Wrapped.BrandedName;
            set { Wrapped.BrandedName = value; }
        }
        [JsonProperty("rateTypeId")]
        public string RateTypeId
        {
            get => Wrapped.RateTypeId;
            set { Wrapped.RateTypeId = value; }
        }
        [JsonProperty("rateTypeBrandedName")]
        public string RateTypeBrandedName
        {
            get => Wrapped.RateTypeBrandedName;
            set { Wrapped.RateTypeBrandedName = value; }
        }
        [JsonProperty("trackable")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Trackable Trackable
        {
            get => Wrapped.Trackable;
            set { Wrapped.Trackable = value; }
        }
        [JsonProperty("specialServiceRules")]
        public IEnumerable<ISpecialServicesRule> SpecialServiceRules
        {
            get => Wrapped.SpecialServiceRules;
            set { Wrapped.SpecialServiceRules = value; }
        }
        [JsonProperty("weightRules")]
        public IEnumerable<IWeightRule> WeightRules
        {
            get => Wrapped.WeightRules;
            set { Wrapped.WeightRules = value; }
        }
        [JsonProperty("dimensionRules")]
        public IEnumerable<IDimensionRule> DimensionRules
        {
            get => Wrapped.DimensionRules;
            set { Wrapped.DimensionRules = value; }
        }
        [JsonProperty("suggestedTrackableSpecialServiceId")]
        public string SuggestedTrackableSpecialServiceId
        {
            get => Wrapped.SuggestedTrackableSpecialServiceId;
            set { Wrapped.SuggestedTrackableSpecialServiceId = value; }
        }
    }
}
