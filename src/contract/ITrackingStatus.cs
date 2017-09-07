using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ITrackingStatus
    {
        string PackageCount { get; set; }
        string Carrier { get; set; }
        string TrackingNumber { get; set; }
        string ReferenceNumber { get; set; }
        TrackingStatusCode Status { get; set; }
        DateTimeOffset UpdatedDateTime { get; set; }
        DateTimeOffset ShipDateTime { get; set; }
        DateTimeOffset EstimatedDeliveryDateTime { get; set; }
        DateTimeOffset DeliveryDateTime { get; set; }
        string DeliveryLocation { get; set; }
        string DeliveryLocationDescription { get; set; }
        string SignedBy { get; set; }
        Decimal Weight { get; set; }
        UnitOfWeight? WeightOUM { get; set; }
        string ReattemptDate { get; set; }
        DateTime ReattemptTime { get; set; }
        IAddress DestinationAddress { get; set; }
        IAddress SenderAddress { get; set; }
        IEnumerable<ITrackingEvent> ScanDetailsList { get; set; }
    }
}
