using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class SpecialServiceCodesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(SpecialServiceCodes).Equals(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.Value)
            {
                case "liveanimal-poultry":
                    return SpecialServiceCodes.liveanimal_poultry;
                case "sunday-holiday":
                    return SpecialServiceCodes.sunday_holiday;
                case "1030":
                    return SpecialServiceCodes.TenThirty;
                case "PO to Addressee":
                    return SpecialServiceCodes.PO_to_Addressee;
                default:
                    var converter = new StringEnumConverter();
                    return converter.ReadJson(reader, objectType, existingValue, serializer);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!typeof(SpecialServiceCodes).Equals(value.GetType())) throw new InvalidOperationException(string.Format("Can't use a USPSSpecialServiceCodesConverter to serialize {0}", value.GetType().ToString())); 
            var s = (SpecialServiceCodes)value;
            switch(s)
            {
                case SpecialServiceCodes.liveanimal_poultry:
                    writer.WriteValue("liveanimal-poultry");
                    break;
                case SpecialServiceCodes.sunday_holiday:
                    writer.WriteValue("sunday-holiday");
                    break;
                case SpecialServiceCodes.TenThirty:
                    writer.WriteValue("1030");
                    break;
                case SpecialServiceCodes.PO_to_Addressee:
                    writer.WriteValue("PO to Addressee");
                    break;
                default:
                    var converter = new StringEnumConverter();
                    converter.WriteJson(writer, value, serializer);
                    break;
            }
        }
    }

}