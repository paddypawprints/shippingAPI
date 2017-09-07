using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CarrierRule : ICarrierRule
    {

        public string ServiceId{get; set;}
        public string BrandedName{get; set;}
        public IEnumerable<IParcelTypeRule> ParcelTypeRules{get; set;}
    }
}
