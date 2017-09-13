using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonParcel<T> : JsonWrapper<T>, IParcel where T:IParcel, new()
    {

        public JsonParcel() : base() { }

        public JsonParcel(T t) : base(t) { }

        [JsonProperty("dimension", Order = 1)]
        public IParcelDimension Dimension
        {
            get => Wrapped.Dimension;
            set { Wrapped.Dimension = value; }
        }

        [JsonProperty("weight", Order = 0)]
        virtual public IParcelWeight Weight
        {
            get => Wrapped.Weight;
            set { Wrapped.Weight = value; }
        }

        public bool ShouldSerializeValueOfGoods() { return ValueOfGoods > 0.0M;  }
        [JsonProperty("valueOfGoods", Order = 2)]
        public decimal ValueOfGoods
        {
            get => Wrapped.ValueOfGoods;
            set { Wrapped.ValueOfGoods = value;  }
        }
        public bool ShouldSerializeCurrencyCode() { return ValueOfGoods > 0.0M; }
        [JsonProperty("currencyCode", Order = 3)]
        public string CurrencyCode
        {
            get => Wrapped.CurrencyCode;
            set { Wrapped.CurrencyCode = value; }
        }
    }


}