using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class TrackingEvent : ITrackingEvent
    {
        virtual public DateTimeOffset EventDateTime{get; set;}
        virtual public string EventCity{get; set;}
        virtual public string EventState{get; set;}
        virtual public string PostalCode{get; set;}
        virtual public string Country{get; set;}
        virtual public string ScanType{get; set;}
        virtual public string ScanDescription{get; set;}
        virtual public string PackageStatus{get; set;}
    }
}
