using System;
using Newtonsoft.Json;
using System.Reflection;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi
{

    internal class ShippingApiConverter : JsonConverter
    {
        private Type _objectType;
        private Type _wrapperType;
        private ShippingApi.Session _session;

        public object Wrap(object o)
        {
            if (_wrapperType == null) return o;
            return Activator.CreateInstance(_wrapperType, new object[] { o, });
        }

        public ShippingApiConverter( Type objectType, Type wrapperType, ShippingApi.Session s )
        {
            _objectType = objectType;
            _wrapperType = wrapperType;
            _session = s;
        }
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = serializer.Deserialize(reader, _wrapperType);
            return o.GetType().GetProperty("Wrapped").GetValue(o);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {   
            serializer.Serialize(writer, Wrap(value));
        }
    }

}