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
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class PackageLocationConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(PackageLocation).Equals(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value)
            {

                case "Front Door":
                    return PackageLocation.FrontDoor;
                case "Back Door":
                    return PackageLocation.BackDoor;
                case "Side Door":
                    return PackageLocation.SideDoor;
                case "Knock on Door / Ring Bell":
                    return PackageLocation.KnockonDoorRingBell;
                case "MailRoom":
                    return PackageLocation.MailRoom;
                case "In / At Mailbox":
                    return PackageLocation.InAtMailbox;
                default:
                    var converter = new StringEnumConverter();
                    return converter.ReadJson(reader, objectType, existingValue, serializer);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!typeof(PackageLocation).Equals(value.GetType())) throw new InvalidOperationException(string.Format("Can't use PackageLocationConverter on a {0}", value.GetType().ToString())); 
            var s = (PackageLocation)value;
            switch (s)
            {
                case PackageLocation.FrontDoor:
                    writer.WriteValue("Front Door");
                    break;
                case PackageLocation.BackDoor:
                    writer.WriteValue("Back Door");
                    break;
                case PackageLocation.SideDoor:
                    writer.WriteValue("Side Door");
                    break;
                case PackageLocation.KnockonDoorRingBell:
                    writer.WriteValue("Knock on Door / Ring Bell");
                    break;
                case PackageLocation.MailRoom:
                    writer.WriteValue("Mail Room");
                    break;
                case PackageLocation.InAtMailbox:
                    writer.WriteValue("In / At Mailbox");
                    break;
                default:
                    var converter = new StringEnumConverter();
                    converter.WriteJson(writer, value, serializer);
                    break;
            }
        }
    }

}