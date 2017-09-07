using System;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ITrackingEvent
    {
        DateTimeOffset EventDateTime { get; set; }
        string EventCity { get; set; }
        string EventState { get; set; }
        string PostalCode { get; set; }
        string Country { get; set; }
        string ScanType { get; set; }
        string ScanDescription { get; set; }
        string PackageStatus { get; set; }
    }
}
