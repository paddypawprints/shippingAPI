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

        virtual public SpecialServiceCodes SpecialServiceId { get; set;}
        virtual public IEnumerable<IParameter> InputParameters { get; set; }
        virtual public void AddParameter(IParameter p)
        {
            ModelHelper.AddToEnumerable<IParameter, Parameter>(p, () => InputParameters, (x) => InputParameters = x);
        }
        virtual public decimal Fee { get; set;}
        virtual public decimal Value
        {
            get
            {
                foreach( var p in InputParameters )
                {
                    if (p.Name == "INPUT_VALUE")
                    {
                        if (decimal.TryParse(p.Value, out decimal value))
                        {
                            return value;
                        }
                    }
                }
                return 0M;
            }
            set
            {
                foreach (var p in InputParameters)
                {
                    if (p.Name == "INPUT_VALUE")
                    {
                        p.Value = value.ToString();
                        return;
                    }
                }
                AddParameter(new Parameter() { Name = "INPUT_VALUE", Value = "0" });
            }
        }
    }
}