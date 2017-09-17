using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ServicesPrerequisiteRule : IServicesPrerequisiteRule
    {
        virtual public string SpecialServiceId{get; set;}
        virtual public string MinInputValue{get; set;}
    }
}
