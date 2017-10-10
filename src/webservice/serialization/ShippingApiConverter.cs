﻿/*
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
using System.Reflection;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi
{

    internal class ShippingApiConverter : JsonConverter
    {
        private Type _objectType;
        private Type _wrapperType;

        public object Wrap(object o)
        {
            if (_wrapperType == null) return o;
            return Activator.CreateInstance(_wrapperType, new object[] { o, });
        }

        public ShippingApiConverter( Type objectType, Type wrapperType )
        {
            _objectType = objectType;
            _wrapperType = wrapperType;
        }
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = serializer.Deserialize(reader, _wrapperType);
            return o.GetType().GetProperty("Wrapped").GetValue(o);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {   
            serializer.Serialize(writer, Wrap(value));
        }
    }

}