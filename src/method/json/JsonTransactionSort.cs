using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonTransactionSort<T> : JsonWrapper<T>, ITransactionSort where T : ITransactionSort, new()
    {
        public JsonTransactionSort() : base() { }
        public JsonTransactionSort(T t) : base(t) { }

        [JsonProperty("ascending")]
        public string Ascending
        {
            get => Wrapped.Ascending;
            set { Wrapped.Ascending = value; }
        }
        [JsonProperty("direction")]
        public string Direction
        {
            get => Wrapped.Direction;
            set { Wrapped.Direction = value; }
        }
        [JsonProperty("ignoreCase")]
        public string IgnoreCase
        {
            get => Wrapped.IgnoreCase;
            set { Wrapped.IgnoreCase = value; }
        }
        [JsonProperty("nullHandling")]
        public string NullHandling
        {
            get => Wrapped.NullHandling;
            set { Wrapped.NullHandling = value; }
        }
        [JsonProperty("property")]
        public string Property
        {
            get => Wrapped.Property;
            set { Wrapped.Property = value; }
        }
    }
}
