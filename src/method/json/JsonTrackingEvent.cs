using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonTrackingEvent<T> : JsonWrapper<T>, ITrackingEvent where T : ITrackingEvent, new()
    {
        public JsonTrackingEvent() : base() { }
        public JsonTrackingEvent(T t) : base(t) { }

        [JsonProperty("eventDateTime")]
        public DateTimeOffset EventDateTime
        {
            get => Wrapped.EventDateTime;
            set { Wrapped.EventDateTime = value; }
        }
        [JsonProperty("eventCity")]
        public string EventCity
        {
            get => Wrapped.EventCity;
            set { Wrapped.EventCity = value; }
        }
        [JsonProperty("eventState")]
        public string EventState
        {
            get => Wrapped.EventState;
            set { Wrapped.EventState = value; }
        }
        [JsonProperty("postalCode")]
        public string PostalCode
        {
            get => Wrapped.PostalCode;
            set { Wrapped.PostalCode = value; }
        }
        [JsonProperty("country")]
        public string Country
        {
            get => Wrapped.Country;
            set { Wrapped.Country = value; }
        }
        [JsonProperty("scanType")]
        public string ScanType
        {
            get => Wrapped.ScanType;
            set { Wrapped.ScanType = value; }
        }
        [JsonProperty("scanDescription")]
        public string ScanDescription
        {
            get => Wrapped.ScanDescription;
            set { Wrapped.ScanDescription = value; }
        }
        [JsonProperty("packageStatus")]
        public string PackageStatus
        {
            get => Wrapped.PackageStatus;
            set { Wrapped.PackageStatus = value; }
        }
    }
}
