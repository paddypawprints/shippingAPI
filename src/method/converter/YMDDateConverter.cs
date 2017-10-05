using System;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class YMDDDateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTimeOffset);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dt = Convert.ToDateTime(reader.Value.ToString());
            if (objectType == typeof(DateTime)) return dt;
            return new DateTimeOffset(dt);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime dt;
            if (value.GetType() == typeof(DateTimeOffset))
            {
                dt = ((DateTimeOffset)value).DateTime;

            }
            else
            {
                dt = (DateTime)value;
            }
            
            writer.WriteValue(string.Format("{0:yyyy-MM-dd}", dt));
        }
    }

}
