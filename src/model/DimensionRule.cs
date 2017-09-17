using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class DimensionRule : IDimensionRule
    {
        virtual public Boolean Required{get; set;}
        virtual public UnitOfDimension UnitOfMeasurement{get; set;}
        virtual public IParcelDimension MinParcelDimensions{get; set;}
        virtual public IParcelDimension MaxParcelDimensions{get; set;}
        virtual public Decimal MinLengthPlusGirth{get; set;}
        virtual public Decimal MaxLengthPlusGirth{get; set;}
    }
}
