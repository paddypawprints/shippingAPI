using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class TrackingStatus : ITrackingStatus
    {

        public string PackageCount{get; set;}
        public string Carrier{get; set;}
        public string TrackingNumber{get; set;}
        public string ReferenceNumber{get; set;}
        public TrackingStatusCode Status{get; set;}
        public DateTimeOffset UpdatedDateTime{get; set;}
        public DateTimeOffset ShipDateTime{get; set;}
        public DateTimeOffset EstimatedDeliveryDateTime{get; set;}
        public DateTimeOffset DeliveryDateTime{get; set;}
        public string DeliveryLocation{get; set;}
        public string DeliveryLocationDescription{get; set;}
        public string SignedBy{get; set;}
        public Decimal Weight{get; set;}
        public UnitOfWeight? WeightOUM{get; set;}
        public string ReattemptDate{get; set;}
        public DateTime ReattemptTime{get; set;}
        public IAddress DestinationAddress{get; set;}
        public IAddress SenderAddress{get; set;}
        public IEnumerable<ITrackingEvent> ScanDetailsList{get; set;}
    }
}
