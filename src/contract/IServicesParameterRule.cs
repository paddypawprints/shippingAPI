using System;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IServicesParameterRule
    {
        string Name { get; set; }
        string BrandedName { get; set; }
        bool Required { get; set; }
        Decimal MinValue { get; set; }
        Decimal MaxValue { get; set; }
        Decimal FreeValue { get; set; }
        string Format { get; set; }
        string Description { get; set; }
    }
}


