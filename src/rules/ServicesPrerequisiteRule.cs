using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ServicesPrerequisiteRule 
    {
        [JsonProperty("specialServiceId")]
        virtual public SpecialServiceCodes SpecialServiceId{get; set;}
        [JsonProperty("minInputValue")]
        virtual public decimal MinInputValue{get; set;}
    }
}
