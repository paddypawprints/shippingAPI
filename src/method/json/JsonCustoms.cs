
using Newtonsoft.Json;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCustoms<T> : JsonWrapper<T>, ICustoms where T: ICustoms, new()
    {
        public JsonCustoms() : base() { }

        public JsonCustoms(T t) : base(t) { }

        [JsonProperty("customsInfo")]
        public ICustomsInfo CustomsInfo
        {
            get => Wrapped.CustomsInfo;
            set { Wrapped.CustomsInfo = value; }
        }
        [JsonProperty("customsItems")]
        public IEnumerable<ICustomsItems> CustomsItems
        {
            get => Wrapped.CustomsItems; 
            set { Wrapped.CustomsItems = value; }
        }
        public ICustomsItems AddCustomsItems(ICustomsItems i)
        {
            return Wrapped.AddCustomsItems(i);
        }
    }
}