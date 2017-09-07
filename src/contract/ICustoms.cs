using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ICustoms
    {
        ICustomsInfo CustomsInfo { get; set; }
        IEnumerable<ICustomsItems> CustomsItems { get; set; }
        ICustomsItems AddCustomsItems(ICustomsItems c);
    }
}