using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonAutoRefill<T> : JsonWrapper<T>, IAutoRefill where T : IAutoRefill, new()
    {
        public JsonAutoRefill() : base() { }
        public JsonAutoRefill(T t) : base(t) { }

        [JsonProperty("merchantID")]
        public string MerchantID
        {
            get => Wrapped.MerchantID;
            set { Wrapped.MerchantID = value; }
        }
        [JsonProperty("threshold")]
        public decimal Threshold
        {
            get => Wrapped.Threshold;
            set { Wrapped.Threshold = value; }
        }
        [JsonProperty("addAmount")]
        public decimal AddAmount
        {
            get => Wrapped.AddAmount;
            set { Wrapped.AddAmount = value; }
        }
        [JsonProperty("enabled")]
        public Boolean Enabled
        {
            get => Wrapped.Enabled;
            set { Wrapped.Enabled = value; }
        }
    }
}
