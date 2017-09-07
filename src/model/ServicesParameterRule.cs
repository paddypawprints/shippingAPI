using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ServicesParameterRule : IServicesParameterRule
    {

        public string Name{get; set;}
        public string BrandedName{get; set;}
        public Boolean Required{get; set;}
        public Decimal MinValue{get; set;}
        public Decimal MaxValue{get; set;}
        public Decimal FreeValue{get; set;}
        public string Format{get; set;}
        public string Description{get; set;}
    }
}
