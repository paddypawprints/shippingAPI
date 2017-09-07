using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonParcel<T> : JsonWrapper<T>, IParcel where T:IParcel, new()
    {

        public JsonParcel() : base() { }

        public JsonParcel(T t) : base(t) { }

        [JsonProperty("dimension")]
        public IParcelDimension Dimension
        {
            get => Wrapped.Dimension;
            set { Wrapped.Dimension = value; }
        }

        [JsonProperty("weight")]
        virtual public IParcelWeight Weight
        {
            get => Wrapped.Weight;
            set { Wrapped.Weight = value; }
        }

        public bool ShouldSerializeValueOfGoods() { return ValueOfGoods > 0.0M;  }
        [JsonProperty("valueOfGoods")]
        public decimal ValueOfGoods
        {
            get => Wrapped.ValueOfGoods;
            set { Wrapped.ValueOfGoods = value;  }
        }
        public bool ShouldSerializeCurrencyCode() { return ValueOfGoods > 0.0M; }
        [JsonProperty("currencyCode")]
        public string CurrencyCode
        {
            get => Wrapped.CurrencyCode;
            set { Wrapped.CurrencyCode = value; }
        }
    }


}