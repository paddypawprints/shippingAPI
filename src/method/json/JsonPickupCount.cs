using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonPickupCount<T> : JsonWrapper<T>, IPickupCount where T : IPickupCount, new()
    {
        public JsonPickupCount() : base() { }
        public JsonPickupCount(T t) : base(t) { }

        [JsonProperty("serviceId")]
        [JsonConverter(typeof(StringEnumConverter))]
        public USPSServices ServiceId
        {
            get => Wrapped.ServiceId;
            set { Wrapped.ServiceId = value; }
        }
        [JsonProperty("count")]
        public int Count
        {
            get => Wrapped.Count;
            set { Wrapped.Count = value; }
        }
        [JsonProperty("totalWeight")]
        public IParcelWeight TotalWeight
        {
            get => Wrapped.TotalWeight;
            set { Wrapped.TotalWeight = value; }
        }
    }
}
