using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class TrackingStatus : ITrackingStatus
    {
        virtual public string PackageCount{get; set;}
        virtual public string Carrier{get; set;}
        virtual public string TrackingNumber{get; set;}
        virtual public string ReferenceNumber{get; set;}
        virtual public TrackingStatusCode Status{get; set;}
        virtual public DateTimeOffset UpdatedDateTime{get; set;}
        virtual public DateTimeOffset ShipDateTime{get; set;}
        virtual public DateTimeOffset EstimatedDeliveryDateTime{get; set;}
        virtual public DateTimeOffset DeliveryDateTime{get; set;}
        virtual public string DeliveryLocation{get; set;}
        virtual public string DeliveryLocationDescription{get; set;}
        virtual public string SignedBy{get; set;}
        virtual public Decimal Weight{get; set;}
        virtual public UnitOfWeight? WeightOUM{get; set;}
        virtual public string ReattemptDate{get; set;}
        virtual public DateTime ReattemptTime{get; set;}
        virtual public IAddress DestinationAddress{get; set;}
        virtual public IAddress SenderAddress{get; set;}
        virtual public IEnumerable<ITrackingEvent> ScanDetailsList{get; set;}
    }
}
