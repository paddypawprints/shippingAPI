using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonDimensionRule<T> : JsonWrapper<T>, IDimensionRule where T : IDimensionRule, new()
    {
        public JsonDimensionRule() : base() { }
        public JsonDimensionRule(T t) : base(t) { }

        [JsonProperty("required")]
        public Boolean Required
        {
            get => Wrapped.Required;
            set { Wrapped.Required = value; }
        }
        [JsonProperty("unitOfMeasurement")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfDimension UnitOfMeasurement
        {
            get => Wrapped.UnitOfMeasurement;
            set { Wrapped.UnitOfMeasurement = value; }
        }
        [JsonProperty("minParcelDimensions")]
        public IParcelDimension MinParcelDimensions
        {
            get => Wrapped.MinParcelDimensions;
            set { Wrapped.MinParcelDimensions = value; }
        }
        [JsonProperty("maxParcelDimensions")]
        public IParcelDimension MaxParcelDimensions
        {
            get => Wrapped.MaxParcelDimensions;
            set { Wrapped.MaxParcelDimensions = value; }
        }
        [JsonProperty("minLengthPlusGirth")]
        public Decimal MinLengthPlusGirth
        {
            get => Wrapped.MinLengthPlusGirth;
            set { Wrapped.MinLengthPlusGirth = value; }
        }
        [JsonProperty("maxLengthPlusGirth")]
        public Decimal MaxLengthPlusGirth
        {
            get => Wrapped.MaxLengthPlusGirth;
            set { Wrapped.MaxLengthPlusGirth = value; }
        }
    }
}
