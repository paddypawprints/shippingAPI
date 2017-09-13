using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class TrackingStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(TrackingStatusCode).Equals(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value)
            {
                case "In Transit":
                    return TrackingStatusCode.InTransit;
                default:
                    var converter = new StringEnumConverter();
                    return converter.ReadJson(reader, objectType, existingValue, serializer);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!typeof(TrackingStatusCode).Equals(value.GetType())) throw new Exception(); //TODO
            var s = (TrackingStatusCode)value;
            switch (s)
            {
                case TrackingStatusCode.InTransit:
                    writer.WriteValue("In Transit");
                    break;
                default:
                    var converter = new StringEnumConverter();
                    converter.WriteJson(writer, value, serializer);
                    break;
            }
        }
    }

}
