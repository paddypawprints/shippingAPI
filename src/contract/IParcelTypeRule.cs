using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IParcelTypeRule
    {
        ParcelType ParcelType { get; set; }
        string BrandedName { get; set; }
        string RateTypeId { get; set; }
        string RateTypeBrandedName { get; set; }
        Trackable Trackable { get; set; }
        IEnumerable<ISpecialServicesRule> SpecialServiceRules { get; set; }
        IEnumerable<IWeightRule> WeightRules { get; set; }
        IEnumerable<IDimensionRule> DimensionRules { get; set; }
        string SuggestedTrackableSpecialServiceId { get; set; }
    }
}


