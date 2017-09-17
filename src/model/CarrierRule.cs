using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CarrierRule : ICarrierRule
    {
        virtual public string ServiceId{get; set;}
        virtual public string BrandedName{get; set;}
        virtual public IEnumerable<IParcelTypeRule> ParcelTypeRules{get; set;}
    }
}
