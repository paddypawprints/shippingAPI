using System;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class UnixMillisecondsTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long t;

            if (reader.Value.GetType() != typeof (long))
                t = long.Parse((string) reader.Value);
            else
                t = (long)reader.Value;
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, new TimeSpan(0, 0, 0)).AddMilliseconds(t);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("UnixMillisecondsTimeConverter serializer not implemented");
        }
 
    }

}