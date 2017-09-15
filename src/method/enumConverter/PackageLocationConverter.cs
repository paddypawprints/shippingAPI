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
            if (!typeof(PackageLocation).Equals(value.GetType())) throw new Exception(); //TODO
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