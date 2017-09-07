using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class SpecialServices : ISpecialServices
    {
        public SpecialServices()
        {
        }

        virtual public USPSSpecialServiceCodes SpecialServiceId { get; set;}

        virtual public IEnumerable<IParameter> InputParameters { get; set; }
        virtual public void AddParameter(IParameter p)
        {
            ModelHelper.AddToEnumerable<IParameter, Parameter>(p, () => InputParameters, (x) => InputParameters = x);
        }
        virtual public decimal Fee { get; set;}
    }

}