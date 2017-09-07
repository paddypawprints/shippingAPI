using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ParcelTypeRule : IParcelTypeRule
    {

        public USPSParcelType ParcelType{get; set;}
        public string BrandedName{get; set;}
        public string RateTypeId{get; set;}
        public string RateTypeBrandedName{get; set;}
        public Trackable Trackable{get; set;}
        public IEnumerable<ISpecialServicesRule> SpecialServiceRules{get; set;}
        public IEnumerable<IWeightRule> WeightRules{get; set;}
        public IEnumerable<IDimensionRule> DimensionRules{get; set;}
        public string SuggestedTrackableSpecialServiceId{get; set;}
    }
}
