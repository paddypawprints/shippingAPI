using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ParcelTypeRule : IRateRule
    {
        [JsonProperty("parcelType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ParcelType ParcelType { get; set; }
        [JsonProperty("brandedName")]
        public string BrandedName { get; set; }
        [JsonProperty("rateTypeId")]
        public string RateTypeId { get; set; }
        [JsonProperty("rateTypeBrandedName")]
        public string RateTypeBrandedName { get; set; }
        [JsonProperty("trackable")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Trackable Trackable { get; set; }
        public IndexedList<SpecialServiceCodes, SpecialServicesRule> SpecialServiceRules { get; set; }
        [JsonProperty("weightRules")]
        public IEnumerable<WeightRule> WeightRules { get; set; }
        [JsonProperty("dimensionRules")]
        public IEnumerable<DimensionRule> DimensionRules { get; set; }
        [JsonProperty("suggestedTrackableSpecialServiceId")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpecialServiceCodes SuggestedTrackableSpecialServiceId { get; set; }
        [JsonProperty("specialServiceRules")]
        internal IEnumerable<SpecialServicesRule> SerializerSpecialServiceRules
        {
            get => SpecialServiceRules;
            set
            {
                if (SpecialServiceRules == null) SpecialServiceRules = new IndexedList<SpecialServiceCodes, SpecialServicesRule>();
                foreach( var ss in value)
                {
                    SpecialServiceRules.Add(ss.SpecialServiceId, ss);
                }
            }
        }
        public void Accept(IRateRuleVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
