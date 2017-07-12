using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.IO;


namespace com.pb.shippingapi
{
    internal class ShippingAPIJsonReader : JsonTextReader
    {
        public class TokenAndValue
        {
            public JsonToken Token { get;set;}
            public Object TokenValue{ get;set; }
        }
        private Stack<TokenAndValue> _pushedTokens = new Stack<TokenAndValue>();

        public ShippingAPIJsonReader(TextReader reader) : base(reader)
        {
        }

        public void Push( JsonToken token, Object value )
        {
            _pushedTokens.Push( new TokenAndValue() { Token = TokenType, TokenValue = Value});
            SetToken(token, value);
        }
        public override bool Read()
        {
            if ( _pushedTokens.Count == 0 )
                return base.Read();
            var current = _pushedTokens.Pop();
            SetToken(current.Token, current.TokenValue);
            return true;
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
            if ( ! (reader is ShippingAPIJsonReader) ) throw new Exception();// TODO:
            var apiReader = (ShippingAPIJsonReader)reader;
            var tokenQueue = new List<ShippingAPIJsonReader.TokenAndValue>();
            try 
            {
                apiReader.Read();
                tokenQueue.Add( new ShippingAPIJsonReader.TokenAndValue() { Token = apiReader.TokenType, TokenValue = apiReader.Value});
                switch( apiReader.TokenType )
                {
                    case JsonToken.StartArray:
                        apiReader.Read();
                        tokenQueue.Add( new ShippingAPIJsonReader.TokenAndValue() { Token = apiReader.TokenType, TokenValue = apiReader.Value});
                        if ( apiReader.TokenType != JsonToken.StartObject ) throw new Exception();//TODO:
                        apiReader.Read();
                        tokenQueue.Add( new ShippingAPIJsonReader.TokenAndValue() { Token = apiReader.TokenType, TokenValue = apiReader.Value});
                        if ( apiReader.TokenType == JsonToken.String )
                        {
                            if (((string)apiReader.Value).Equals("errorCode"))
                            {
                                return typeof ( ErrorFormat1);
                            }
                            else if (((string)apiReader.Value).Equals("key"))
                            {
                                return typeof (ErrorFormat3);
                            }

                        }
            
                        throw new Exception(); //TODO:
                    case JsonToken.StartObject:
                        apiReader.Read();
                        tokenQueue.Add( new ShippingAPIJsonReader.TokenAndValue() { Token = apiReader.TokenType, TokenValue = apiReader.Value});
                        if ( apiReader.TokenType == JsonToken.String )
                        {
                            if (((string)apiReader.Value).Equals("errors"))
                            {
                                return typeof ( ErrorFormat2);
                            }
                            else 
                            {
                                return typeof (ShippingAPIResponse<Response>);
                            }

                        }
            
                        throw new Exception(); //TODO:
                    default:
                            throw new Exception(); //TODO: sort out exceptions
                    }
                }
                finally
                {
                    foreach( var token in tokenQueue )
                    {
                        apiReader.Push( token.Token, token.TokenValue );
                    }
                }
            }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Type));
        }
    }

}