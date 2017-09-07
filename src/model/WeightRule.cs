using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class WeightRule : IWeightRule
    {

        public Boolean Required{get; set;}
        public UnitOfWeight UnitOfWeight{get; set;}
        public Decimal MinWeight{get; set;}
        public Decimal MaxWeight{get; set;}
    }
}
