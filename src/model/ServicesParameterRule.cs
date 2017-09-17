using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ServicesParameterRule : IServicesParameterRule
    {
        virtual public string Name{get; set;}
        virtual public string BrandedName{get; set;}
        virtual public Boolean Required{get; set;}
        virtual public Decimal MinValue{get; set;}
        virtual public Decimal MaxValue{get; set;}
        virtual public Decimal FreeValue{get; set;}
        virtual public string Format{get; set;}
        virtual public string Description{get; set;}
    }
}
