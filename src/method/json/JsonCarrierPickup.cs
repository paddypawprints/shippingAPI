using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCarrierPickup<T> : JsonWrapper<T>, ICarrierPickup where T :  ICarrierPickup, new()
    {
        public JsonCarrierPickup() : base() { }
        public JsonCarrierPickup(T t) : base(t) { }

        [JsonProperty("pickupAddress")]
        public IAddress PickupAddress
        {
            get => Wrapped.PickupAddress;
            set { Wrapped.PickupAddress = value; }
        }
        [JsonProperty("carrier")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Carrier Carrier
        {
            get => Wrapped.Carrier;
            set { Wrapped.Carrier = value; }
        }
        [JsonProperty("pickupSummary")]
        public IEnumerable<IPickupCount> PickupSummary
        {
            get => Wrapped.PickupSummary;
            set { Wrapped.PickupSummary = value; }
        }
        [JsonProperty("required")]
        public string Required
        {
            get => Wrapped.Required;
            set { Wrapped.Required = value; }
        }
        [JsonProperty("packageLocation")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PackageLocation PackageLocation
        {
            get => Wrapped.PackageLocation;
            set { Wrapped.PackageLocation = value; }
        }
        [JsonProperty("specialInstructions")]
        public string SpecialInstructions
        {
            get => Wrapped.SpecialInstructions;
            set { Wrapped.SpecialInstructions = value; }
        }
        [JsonProperty("pickupDateTime")]
        public DateTimeOffset PickupDateTime
        {
            get => Wrapped.PickupDateTime;
            set { Wrapped.PickupDateTime = value; }
        }
        [JsonProperty("pickupConfirmationNumber")]
        public string PickupConfirmationNumber
        {
            get => Wrapped.PickupConfirmationNumber;
            set { Wrapped.PickupConfirmationNumber = value; }
        }
        [JsonProperty("pickupId")]
        public string PickupId
        {
            get => Wrapped.PickupId;
            set { Wrapped.PickupId = value; }
        }
    }
}
