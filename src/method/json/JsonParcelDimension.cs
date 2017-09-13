
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonParcelDimension<T> : JsonWrapper<T>, IParcelDimension where T : IParcelDimension, new()
    {
        public JsonParcelDimension() : base() { }

        public JsonParcelDimension(T t) : base(t) { }

        public JsonParcelDimension(decimal l, decimal h, decimal w,  UnitOfDimension u = UnitOfDimension.IN) : base()
        {
            Wrapped.Length = l;
            Wrapped.Height = h;
            Wrapped.Width = w;
            Wrapped.UnitOfMeasurement = u;
            Wrapped.IrregularParcelGirth = 0.0M;
        }

        [JsonProperty(PropertyName ="length", Order = 1)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Length
        {
            get => Wrapped.Length;
            set { Wrapped.Length = value; }
        }
        [JsonProperty(PropertyName ="height", Order = 2)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Height
        {
            get => Wrapped.Height;
            set { Wrapped.Height = value; }
        }
        [JsonProperty(PropertyName ="width", Order = 3)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Width
        {
            get => Wrapped.Width;
            set { Wrapped.Width = value; }
        }
        [JsonProperty(PropertyName ="unitOfMeasurement", Order = 0)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfDimension UnitOfMeasurement
        {
            get => Wrapped.UnitOfMeasurement;
            set { Wrapped.UnitOfMeasurement = value; }
        }
        public bool ShouldSerializeIrregularParcelGirth() { return IrregularParcelGirth > 0.0M; }

        [JsonProperty(PropertyName ="irregularParcelGirth", Order = 4)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal IrregularParcelGirth
        {
            get => Wrapped.IrregularParcelGirth;
            set { Wrapped.IrregularParcelGirth = value; }
        }

    }
}