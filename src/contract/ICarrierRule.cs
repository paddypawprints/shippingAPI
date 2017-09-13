using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ICarrierRule
    {
        string ServiceId { get; set; }
        string BrandedName { get; set; }
        IEnumerable<IParcelTypeRule> ParcelTypeRules { get; set; }
    }
}


