using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.IO;


namespace com.pb.shippingapi
{
    internal class UnixSecondsTimeConverter : Newtonsoft.Json.JsonConverter
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
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, new TimeSpan(0, 0, 0)).AddSeconds(t);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    internal class UnixMillisecondsTimeConverter : Newtonsoft.Json.JsonConverter
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
            throw new NotImplementedException();
        }
 
    }
    internal class ShippingAPIResponseTypeConverter<Response> : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
           reader.Read();
            switch( reader.TokenType )
            {
                case JsonToken.StartArray:
                    reader.Read();
                    if ( reader.TokenType != JsonToken.StartObject ) throw new Exception();//TODO:
                    reader.Read();
                    if ( reader.TokenType == JsonToken.String )
                    {
                        if (((string)reader.Value).Equals("errorCode"))
                        {
                            return typeof ( ErrorFormat1);
                        }
                        else if (((string)reader.Value).Equals("key"))
                        {
                            return typeof (ErrorFormat3);
                        }

                    }
        
                    throw new Exception(); //TODO:
                case JsonToken.StartObject:
                    reader.Read();
                    if ( reader.TokenType == JsonToken.PropertyName )
                    {
                        if (((string)reader.Value).Equals("errors"))
                        {
                            return typeof ( ErrorFormat2);
                        }
                        else 
                        {
                            return typeof (Response);
                        }

                    }
        
                    throw new Exception(); //TODO:
                default:
                        throw new Exception(); //TODO: sort out exceptions
                }

            }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Type));
        }
    }

}