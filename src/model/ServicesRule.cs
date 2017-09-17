using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ServicesRule : ISpecialServicesRule
    {
        virtual public string SpecialServiceId{get; set;}
        virtual public string BrandedName{get; set;}
        virtual public string CategoryId{get; set;}
        virtual public string CategoryName{get; set;}
        virtual public Boolean Trackable{get; set;}
        virtual public IEnumerable<IServicesParameterRule> InputParameterRules{get; set;}
        virtual public IEnumerable<IServicesPrerequisiteRule> PrerequisiteRules{get; set;}
        virtual public IEnumerable<SpecialServiceCodes> IncompatibleSpecialServices{get; set;}
    }
}
