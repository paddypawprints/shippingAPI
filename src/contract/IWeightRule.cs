using System;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IWeightRule
    {
        bool Required { get; set; }
        UnitOfWeight UnitOfWeight { get; set; }
        Decimal MinWeight { get; set; }
        Decimal MaxWeight { get; set; }
    }
}


