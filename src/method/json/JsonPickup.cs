using System;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonPickup<T> : JsonWrapper<T>, IShippingApiRequest, IPickup where T : IPickup, new()
    {
        public JsonPickup() : base() { }
        public JsonPickup(T t) : base(t) { }

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
        [JsonProperty("reference")]
        public string Reference
        {
            get => Wrapped.Reference;
            set { Wrapped.Reference = value; }
        }
        [JsonProperty("packageLocation")]
        [JsonConverter(typeof(PackageLocationConverter))]
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

        public bool ShouldSerializePickupDate() => false;

        [JsonProperty("pickupDate")]
        [JsonConverter(typeof(CalendarDateConverter))]
        public DateTime PickupDate
        {
            get => Wrapped.PickupDate;
            set { Wrapped.PickupDate = value; }
        }

        public bool ShouldSerializePickupConfirmationNumber() => false;

        [JsonProperty("pickupConfirmationNumber")]
        public string PickupConfirmationNumber
        {
            get => Wrapped.PickupConfirmationNumber;
            set { Wrapped.PickupConfirmationNumber = value; }
        }

        public bool ShouldSerializePickupId() => false;

        [JsonProperty("pickupId")]
        public string PickupId
        {
            get => Wrapped.PickupId;
            set { Wrapped.PickupId = value; }
        }

        public string GetUri(string baseUrl)
        {
            StringBuilder uri = new StringBuilder(baseUrl);
            ShippingApiRequest.AddRequestResource(this, uri);
            ShippingApiRequest.AddRequestQuery(this, uri);
            return uri.ToString();
        }
        public IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }
        public void SerializeBody(StreamWriter writer, ShippingApi.Session session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }


        public string ContentType => "application/json";

        [ShippingApiHeaderAttribute("Bearer")]
        public StringBuilder Authorization { get; set; }

        public void AddPickupCount(IPickupCount p)
        {
            Wrapped.AddPickupCount(p);
        }
    }
}
