using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ServicesRule : ISpecialServicesRule
    {

        public string SpecialServiceId{get; set;}
        public string BrandedName{get; set;}
        public string CategoryId{get; set;}
        public string CategoryName{get; set;}
        public Boolean Trackable{get; set;}
        public IEnumerable<IServicesParameterRule> InputParameterRules{get; set;}
        public IEnumerable<IServicesPrerequisiteRule> PrerequisiteRules{get; set;}
        public IEnumerable<USPSSpecialServiceCodes> IncompatibleSpecialServices{get; set;}
    }
}
