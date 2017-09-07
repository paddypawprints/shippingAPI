
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Customs : ICustoms
    {
        public Customs()
        {
            CustomsItems = new List<CustomsItems>();
        }

        virtual public ICustomsInfo CustomsInfo { get; set;}
        virtual public IEnumerable<ICustomsItems> CustomsItems { get; set; }
        virtual public ICustomsItems AddCustomsItems(ICustomsItems c)
        {
            return ModelHelper.AddToEnumerable<ICustomsItems, CustomsItems>(c, () => CustomsItems, (x) => CustomsItems = x);
        }
    }
}