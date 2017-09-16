using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ISpecialServicesRule
    {
        string SpecialServiceId { get; set; }
        string BrandedName { get; set; }
        string CategoryId { get; set; }
        string CategoryName { get; set; }
        bool Trackable { get; set; }
        IEnumerable<IServicesParameterRule> InputParameterRules { get; set; }
        IEnumerable<IServicesPrerequisiteRule> PrerequisiteRules { get; set; }
        IEnumerable<SpecialServiceCodes> IncompatibleSpecialServices { get; set; }
    }
}


