using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class DimensionRule : IDimensionRule
    {

        public Boolean Required{get; set;}
        public UnitOfDimension UnitOfMeasurement{get; set;}
        public IParcelDimension MinParcelDimensions{get; set;}
        public IParcelDimension MaxParcelDimensions{get; set;}
        public Decimal MinLengthPlusGirth{get; set;}
        public Decimal MaxLengthPlusGirth{get; set;}
    }
}
