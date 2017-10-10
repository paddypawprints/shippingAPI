﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SpecialServicesRule : IRateRule
    {
        [JsonProperty("specialServiceId")]
        public SpecialServiceCodes SpecialServiceId { get; set; }
        [JsonProperty("brandedName")]
        public string BrandedName { get; set; }
        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
        [JsonProperty("trackable")]
        public bool Trackable { get; set; }
        public Dictionary<string, ServicesParameterRule> InputParameterRules { get; set; }
        public IndexedList<SpecialServiceCodes, ServicesPrerequisiteRule> PrerequisiteRules { get; set; }
        [JsonProperty("incompatibleSpecialServices")]
        public IEnumerable<SpecialServiceCodes> IncompatibleSpecialServices { get; set; }
        [JsonProperty("inputParameterRules")]
        internal IEnumerable<ServicesParameterRule> SerializerInputParameterRules
        {
            get
            {
                if (InputParameterRules == null) return null;
                else return InputParameterRules.Values;
            }
            set
            {
                if (InputParameterRules == null) InputParameterRules = new Dictionary<string, ServicesParameterRule>();
                foreach(var p in value)
                {
                    InputParameterRules.Add(p.Name, p);
                }
            }
        }
        [JsonProperty("prerequisiteRules")]
        internal IEnumerable<ServicesPrerequisiteRule> SerializerPrerequisiteRules
        {
            get => PrerequisiteRules;
            set
            {
                if (PrerequisiteRules == null) PrerequisiteRules = new IndexedList<SpecialServiceCodes, ServicesPrerequisiteRule>();
                foreach(var p in value)
                {
                    PrerequisiteRules.Add(p.SpecialServiceId, p);
                }
            }
        }

        public void Accept(IRateRuleVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsValidParameters( ISpecialServices services)
        {
            Dictionary<string, bool> foundRequired = new Dictionary<string, bool>();
            foreach (var p in InputParameterRules.Values)
            {
                if (p.Required) foundRequired.Add(p.Name, false);
            }
            foreach (var ip in services.InputParameters)
            {
                if (!InputParameterRules.ContainsKey(ip.Name)) return false;
                if (foundRequired.ContainsKey(ip.Name)) foundRequired[ip.Name] = true;
                if (decimal.TryParse(ip.Value, out decimal value))
                {
                    if (value < InputParameterRules[ip.Name].MinValue) return false;
                    if (value > InputParameterRules[ip.Name].MaxValue) return false;
                }
            }
            foreach (var f in foundRequired)
            {
                if (!f.Value) return false;
            }
            return true;
        }
        public bool IsValidPrerequisites(IEnumerable<ISpecialServices> services)
        {
            foreach(var ss in services)
            {
                if (!PrerequisiteRules.ContainsKey(ss.SpecialServiceId)) continue;
                foreach(var r in PrerequisiteRules[ss.SpecialServiceId])
                {
                    if ( ss.Value < r.MinInputValue ) return false;
                }
            }
            return true;
        }
    }
}
