using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Rules
{

    [JsonObject(MemberSerialization.OptIn)]
    public class ServiceRule : IRateRule
    {

        [JsonProperty("serviceId")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Services ServiceId { get; set; }
        [JsonProperty("brandedName")]
        public string BrandedName { get; set; }
        public IndexedList<ParcelType, ParcelTypeRule> ParcelTypeRules { get; internal set; }

        [JsonProperty("parcelTypeRules")]
        internal IEnumerable<ParcelTypeRule> SerializerParcelTypeRules {
            get => ParcelTypeRules;
            set
            {
                if (ParcelTypeRules == null) ParcelTypeRules = new IndexedList<ParcelType, ParcelTypeRule>();
                foreach( var r in value)
                {
                    ParcelTypeRules.Add(r.ParcelType, r);
                }
                
            }
        }

        public void Accept(IRateRuleVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
