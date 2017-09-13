
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonParcelWeight<T> : JsonWrapper<T>, IParcelWeight where T : IParcelWeight, new()
    {
        public JsonParcelWeight() : base() { }

        public JsonParcelWeight(T t) : base(t) { }

        public JsonParcelWeight( decimal w, UnitOfWeight u = UnitOfWeight.OZ) : base()
        {
            Weight = w;
            UnitOfMeasurement = u;
        }
        [JsonProperty(PropertyName ="weight", Order = 1)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Weight
        {
            get => Wrapped.Weight;
            set { Wrapped.Weight = value; }
        }
        [JsonProperty(PropertyName ="unitOfMeasurement", Order = 0)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfWeight UnitOfMeasurement
        {
            get => Wrapped.UnitOfMeasurement;
            set { Wrapped.UnitOfMeasurement = value; }
        }
    }
}