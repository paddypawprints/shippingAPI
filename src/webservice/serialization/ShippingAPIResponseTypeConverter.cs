/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    abstract internal class ShippingApiTypeDetector
    {
        protected bool _failed = false;
        abstract public Type NextToken(int i, JsonToken token, object value, Type defaultType = null);
    }

    internal class ArrayTypeDetector : ShippingApiTypeDetector
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
                    if (token == JsonToken.PropertyName)
                    {
                        if (((string)value).Equals("errorCode"))
                        {
                            return typeof(ErrorFormat1);
                        }
                        else if (((string)value).Equals("key"))
                        {
                            return typeof(ErrorFormat3);
                        }
                        else return defaultType;
                    }
                    break;
                default:
                    _failed = true;
                    break;
            }
            return null;
        }
    }
    internal class ObjectTypeDetector : ShippingApiTypeDetector
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

    internal class ShippingApiResponseTypeConverter<Response> : JsonConverter
    {
        // chain of responsibility - easy to add new types. Make it config item if necessary.
        public List<ShippingApiTypeDetector> Detectors { get; set; }

        public const int MAX_TOKENS = 10;

        public ShippingApiResponseTypeConverter()
        {
            Detectors = new List<ShippingApiTypeDetector>() { new ArrayTypeDetector(), new ObjectTypeDetector() };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("ShippingApiResponseTypeConverter does not support serialization");
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
                if (i > MAX_TOKENS) throw new InvalidOperationException("ShippingApiResponseTypeConverter only looks at " + MAX_TOKENS + " tokens");
            }

            throw new InvalidOperationException("ShippingApiResponseTypeConverter type not found - unexpected end of stream");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Type));
        }
    }

}