using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ParcelTypeRule : IParcelTypeRule
    {
        virtual public ParcelType ParcelType{get; set;}
        virtual public string BrandedName{get; set;}
        virtual public string RateTypeId{get; set;}
        virtual public string RateTypeBrandedName{get; set;}
        virtual public Trackable Trackable{get; set;}
        virtual public IEnumerable<ISpecialServicesRule> SpecialServiceRules{get; set;}
        virtual public IEnumerable<IWeightRule> WeightRules{get; set;}
        virtual public IEnumerable<IDimensionRule> DimensionRules{get; set;}
        virtual public string SuggestedTrackableSpecialServiceId{get; set;}
    }
}
