using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class TrackingEvent : ITrackingEvent
    {

        public DateTimeOffset EventDateTime{get; set;}
        public string EventCity{get; set;}
        public string EventState{get; set;}
        public string PostalCode{get; set;}
        public string Country{get; set;}
        public string ScanType{get; set;}
        public string ScanDescription{get; set;}
        public string PackageStatus{get; set;}
    }
}
