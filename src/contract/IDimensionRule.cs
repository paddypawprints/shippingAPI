using System;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IDimensionRule
    {
        bool Required { get; set; }
        UnitOfDimension UnitOfMeasurement { get; set; }
        IParcelDimension MinParcelDimensions { get; set; }
        IParcelDimension MaxParcelDimensions { get; set; }
        Decimal MinLengthPlusGirth { get; set; }
        Decimal MaxLengthPlusGirth { get; set; }
    }
}


