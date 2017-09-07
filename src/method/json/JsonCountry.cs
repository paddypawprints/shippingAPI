
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCountry<T> : JsonWrapper<T>, ICountry where T : ICountry, new()
    {
        public JsonCountry() : base() { }

        public JsonCountry(T t) : base(t) { }

        [JsonProperty("countryCode")]
        virtual public string CountryCode
        {
            get => Wrapped.CountryCode;
            set { Wrapped.CountryCode = value; }
        }
        [JsonProperty("countryName")]
        virtual public string CountryName
        {
            get => Wrapped.CountryName;
            set { Wrapped.CountryName = value; }
        }
    }


}