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