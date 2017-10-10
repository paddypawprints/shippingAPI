using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DimensionRule
    {
        [JsonProperty("required")]
        virtual public Boolean Required{get; set;}
        [JsonProperty("unitOfMeasurement")]
        virtual public UnitOfDimension UnitOfMeasurement{get; set;}
        [JsonProperty("minParcelDimensions")]
        virtual public IParcelDimension MinParcelDimensions{get; set;}
        [JsonProperty("maxParcelDimensions")]
        virtual public IParcelDimension MaxParcelDimensions{get; set;}
        [JsonProperty("minLengthPlusGirth")]
        virtual public Decimal MinLengthPlusGirth{get; set;}
        [JsonProperty("maxLengthPlusGirth")]
        virtual public Decimal MaxLengthPlusGirth{get; set;}
    }
}
