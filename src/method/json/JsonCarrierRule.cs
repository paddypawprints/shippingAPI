using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCarrierRule<T> : JsonWrapper<T>, ICarrierRule where T : ICarrierRule, new()
    {
        public JsonCarrierRule() : base() { }
        public JsonCarrierRule(T t) : base(t) { }

        public bool ShouldSerializeCarrier() => false;

        public Carrier Carrier
        {
            get => Wrapped.Carrier;
            set { Wrapped.Carrier = value; }
        }

        [JsonProperty("serviceId")]
        public string ServiceId
        {
            get => Wrapped.ServiceId;
            set { Wrapped.ServiceId = value; }
        }
        [JsonProperty("brandedName")]
        public string BrandedName
        {
            get => Wrapped.BrandedName;
            set { Wrapped.BrandedName = value; }
        }
        [JsonProperty("parcelTypeRules")]
        public IEnumerable<IParcelTypeRule> ParcelTypeRules
        {
            get => Wrapped.ParcelTypeRules;
            set { Wrapped.ParcelTypeRules = value; }
        }
    }
}
