using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ISpecialServices
    {
        USPSSpecialServiceCodes SpecialServiceId { get; set; }
        IEnumerable<IParameter> InputParameters { get; set; }
        void AddParameter(IParameter p);
        decimal Fee { get; set; }

    }

}