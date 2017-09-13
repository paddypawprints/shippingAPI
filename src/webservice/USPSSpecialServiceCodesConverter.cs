using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class USPSSpecialServiceCodesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(USPSSpecialServiceCodes).Equals(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.Value)
            {
                case "liveanimal-poultry":
                    return USPSSpecialServiceCodes.liveanimal_poultry;
                case "sunday-holiday":
                    return USPSSpecialServiceCodes.sunday_holiday;
                default:
                    var converter = new StringEnumConverter();
                    return converter.ReadJson(reader, objectType, existingValue, serializer);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!typeof(USPSSpecialServiceCodes).Equals(value.GetType())) throw new Exception(); //TODO
            var s = (USPSSpecialServiceCodes)value;
            switch(s)
            {
                case USPSSpecialServiceCodes.liveanimal_poultry:
                    writer.WriteValue("liveanimal-poultry");
                    break;
                case USPSSpecialServiceCodes.sunday_holiday:
                    writer.WriteValue("sunday-holiday");
                    break;
                default:
                    var converter = new StringEnumConverter();
                    converter.WriteJson(writer, value, serializer);
                    break;
            }
        }
    }

}