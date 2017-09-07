using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ServicesPrerequisiteRule : IServicesPrerequisiteRule
    {

        public string SpecialServiceId{get; set;}
        public string MinInputValue{get; set;}
    }
}
