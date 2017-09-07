
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCustomsItems<T> : JsonWrapper<T>, ICustomsItems where T : ICustomsItems, new()
    {
        public JsonCustomsItems() : base() { }

        public JsonCustomsItems(T t) : base(t) { }

        [JsonProperty("description")]
        public string Description
        {
            get => Wrapped.Description;
            set { Wrapped.Description = value; }
        }
        [JsonProperty("quantity")]
        public int Quantity
        {
            get => Wrapped.Quantity;
            set { Wrapped.Quantity = value; }
        }
        [JsonProperty("unitPrice")]
        public decimal UnitPrice
        {
            get => Wrapped.UnitPrice;
            set { Wrapped.UnitPrice = value; } 
        }
        [JsonProperty("unitWeight")]
        public IParcelWeight UnitWeight
        {
            get => Wrapped.UnitWeight;
            set { Wrapped.UnitWeight = value; }
        }
        [JsonProperty("hSTariffCode")]
        public string HSTariffCode
        {
            get => Wrapped.HSTariffCode;
            set { Wrapped.HSTariffCode = value; }
        }
        [JsonProperty("originCountryCode")]
        public string OriginCountryCode
        {
            get => Wrapped.OriginCountryCode;
            set { Wrapped.OriginCountryCode = value; } 
        }
    }

}