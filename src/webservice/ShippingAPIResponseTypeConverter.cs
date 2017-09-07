using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    abstract internal class ShippingAPITypeDetector
    {
        protected bool _failed = false;
        abstract public Type NextToken(int i, JsonToken token, object value, Type defaultType = null);
    }

    class ArrayTypeDetector : ShippingAPITypeDetector
    {
        public override Type NextToken(int i, JsonToken token, object value, Type defaultType = null)
        {
            if (_failed) return null;
            switch (i)
            {
                case 0:
                    _failed = (token != JsonToken.StartArray);
                    break;
                case 1:
                    _failed = (token != JsonToken.StartObject);
                    break;
                case 2:
                    if (token == JsonToken.String)
                    {
                        if (((string)value).Equals("errorCode"))
                        {
                            return typeof(ErrorFormat1);
                        }
                        else if (((string)value).Equals("key"))
                        {
                            return typeof(ErrorFormat3);
                        }
                        else if (((string)value).Equals("countryCode"))
                        {
                            return typeof(IEnumerable<ICountry>);
                        }
                        else if (((string)value).Equals("serviceId"))
                        {
                            return typeof(IEnumerable<ICarrierRule>);
                        }

                        else _failed = true;
                    }
                    break;
                default:
                    _failed = true;
                    break;
            }
            return null;
        }
    }
    class ObjectTypeDetector : ShippingAPITypeDetector
    {
        public override Type NextToken(int i, JsonToken token, object value, Type defaultType = null)
        {
            if (_failed) return null;
            switch (i)
            {
                case 0:
                    _failed = (token != JsonToken.StartObject);
                    break;
                case 1:
                    if (token == JsonToken.PropertyName)
                    {
                        if (((string)value).Equals("errors"))
                        {
                            return typeof(ErrorFormat2);
                        }
                        else
                        {
                            return defaultType;
                        }
                    }
                    else
                    {
                        _failed = true;
                    }
                    break;
                default:
                    _failed = true;
                    break;
            }
            return null;
        }
    }

    internal class ShippingAPIResponseTypeConverter<Response> : JsonConverter
    {
        // chain of responsibility - easy to add new types. Make it config item if necessary.
        public List<ShippingAPITypeDetector> Detectors { get; set; }

        public const int MAX_TOKENS = 10;

        public ShippingAPIResponseTypeConverter()
        {
            Detectors = new List<ShippingAPITypeDetector>() { new ArrayTypeDetector(), new ObjectTypeDetector() };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            int i = 0;
            while (reader.Read())
            {
                foreach( var d in Detectors)
                {
                    var t = d.NextToken(i, reader.TokenType, reader.Value, typeof(Response));
                    if (t != null) return t;
                }
                i++;
                if (i > MAX_TOKENS) throw new Exception();
            }
            throw new Exception(); // TODO: logging etc.
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Type));
        }
    }

}