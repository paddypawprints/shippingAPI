using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonTrackingStatus<T> : JsonWrapper<T>, ITrackingStatus where T : ITrackingStatus, new()
    {
        public JsonTrackingStatus() : base() { }
        public JsonTrackingStatus(T t) : base(t) { }

        [JsonProperty("packageCount")]
        public string PackageCount
        {
            get => Wrapped.PackageCount;
            set { Wrapped.PackageCount = value; }
        }
        [JsonProperty("carrier")]
        public string Carrier
        {
            get => Wrapped.Carrier;
            set { Wrapped.Carrier = value; }
        }
        [JsonProperty("trackingNumber")]
        public string TrackingNumber
        {
            get => Wrapped.TrackingNumber;
            set { Wrapped.TrackingNumber = value; }
        }
        [JsonProperty("referenceNumber")]
        public string ReferenceNumber
        {
            get => Wrapped.ReferenceNumber;
            set { Wrapped.ReferenceNumber = value; }
        }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TrackingStatusCode Status
        {
            get => Wrapped.Status;
            set { Wrapped.Status = value; }
        }
        [JsonProperty("updatedDateTime")]
        public DateTimeOffset UpdatedDateTime
        {
            get => Wrapped.UpdatedDateTime;
            set { Wrapped.UpdatedDateTime = value; }
        }
        [JsonProperty("shipDateTime")]
        public DateTimeOffset ShipDateTime
        {
            get => Wrapped.ShipDateTime;
            set { Wrapped.ShipDateTime = value; }
        }
        [JsonProperty("estimatedDeliveryDateTime")]
        public DateTimeOffset EstimatedDeliveryDateTime
        {
            get => Wrapped.EstimatedDeliveryDateTime;
            set { Wrapped.EstimatedDeliveryDateTime = value; }
        }
        [JsonProperty("deliveryDateTime")]
        public DateTimeOffset DeliveryDateTime
        {
            get => Wrapped.DeliveryDateTime;
            set { Wrapped.DeliveryDateTime = value; }
        }
        [JsonProperty("deliveryLocation")]
        public string DeliveryLocation
        {
            get => Wrapped.DeliveryLocation;
            set { Wrapped.DeliveryLocation = value; }
        }
        [JsonProperty("deliveryLocationDescription")]
        public string DeliveryLocationDescription
        {
            get => Wrapped.DeliveryLocationDescription;
            set { Wrapped.DeliveryLocationDescription = value; }
        }
        [JsonProperty("signedBy")]
        public string SignedBy
        {
            get => Wrapped.SignedBy;
            set { Wrapped.SignedBy = value; }
        }
        [JsonProperty("weight")]
        public Decimal Weight
        {
            get => Wrapped.Weight;
            set { Wrapped.Weight = value; }
        }
        [JsonProperty("weightOUM")]
        public UnitOfWeight? WeightOUM
        {
            get => Wrapped.WeightOUM;
            set { Wrapped.WeightOUM = value; }
        }
        [JsonProperty("reattemptDate")]
        public string ReattemptDate
        {
            get => Wrapped.ReattemptDate;
            set { Wrapped.ReattemptDate = value; }
        }
        [JsonProperty("reattemptTime")]
        public DateTime ReattemptTime
        {
            get => Wrapped.ReattemptTime;
            set { Wrapped.ReattemptTime = value; }
        }
        [JsonProperty("destinationAddress")]
        public IAddress DestinationAddress
        {
            get => Wrapped.DestinationAddress;
            set { Wrapped.DestinationAddress = value; }
        }
        [JsonProperty("senderAddress")]
        public IAddress SenderAddress
        {
            get => Wrapped.SenderAddress;
            set { Wrapped.SenderAddress = value; }
        }
        [JsonProperty("scanDetailsList")]
        public IEnumerable<ITrackingEvent> ScanDetailsList
        {
            get => Wrapped.ScanDetailsList;
            set { Wrapped.ScanDetailsList = value; }
        }
    }
}
