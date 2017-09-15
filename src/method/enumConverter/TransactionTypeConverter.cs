using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class TransactionTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(TransactionType).Equals(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value)
            {
                case "POSTAGE FUND":
                    return TransactionType.POSTAGE_FUND;
                case "POSTAGE PRINT":
                    return TransactionType.POSTAGE_PRINT;
                case "POSTAGE REFUND":
                    return TransactionType.POSTAQGE_REFUND;
                default:
                    var converter = new StringEnumConverter();
                    return converter.ReadJson(reader, objectType, existingValue, serializer);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!typeof(TransactionType).Equals(value.GetType())) throw new Exception(); //TODO
            var s = (TransactionType)value;
            switch (s)
            {
                case TransactionType.POSTAGE_FUND:
                    writer.WriteValue("POSTAGE FUND");
                    break;
                case TransactionType.POSTAGE_PRINT:
                    writer.WriteValue("POSTAGE PRINT");
                    break;
                case TransactionType.POSTAQGE_REFUND:
                    writer.WriteValue("POSTAGE REFUND");
                    break;
                default:
                    var converter = new StringEnumConverter();
                    converter.WriteJson(writer, value, serializer);
                    break;
            }
        }
    }

}