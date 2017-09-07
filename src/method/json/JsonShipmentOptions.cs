using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonShipmentOptions<T> : JsonWrapper<T>, IShipmentOptions where T: IShipmentOptions, new()
    {
        public JsonShipmentOptions() : base() { }

        public JsonShipmentOptions(T t) : base(t) { }

        public JsonShipmentOptions(ShipmentOption o, string v) : base()
        {
            Wrapped.ShipmentOption = o;
            Wrapped.Value = v;
        }
        public void Copy(IShipmentOptions o)
        {
            ShipmentOption = o.ShipmentOption;
            Value = o.Value;
        }
        [JsonProperty("name")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ShipmentOption ShipmentOption
        {
            get => Wrapped.ShipmentOption;
            set { Wrapped.ShipmentOption = value; }
        }
        [JsonProperty("value")]
        public string Value
        {
            get => Wrapped.Value;
            set { Wrapped.Value = value; }
        }
    }

}