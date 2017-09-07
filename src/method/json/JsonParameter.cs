using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json

{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonParameter<T> : JsonWrapper<T>, IParameter where T:IParameter, new()
    {
        
        public JsonParameter() : base() { }

        public JsonParameter(T t) : base(t) { }

        public JsonParameter( string name, string value) : base()
        {
            Wrapped.Name = name;
            Wrapped.Value = value;
        }

        [JsonProperty("name")]
        public string Name
        {
            get =>Wrapped.Name;
            set { Wrapped.Name = value; }
        }
        [JsonProperty("value")]
        public string Value
        {
            get => Wrapped.Value;
            set { Wrapped.Value = value; }
        }
    }

}