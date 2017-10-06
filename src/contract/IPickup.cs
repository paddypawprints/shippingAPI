using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IPickup
    {
        string TransactionId { get; set; }
        IAddress PickupAddress { get; set; }
        Carrier Carrier { get; set; }
        IEnumerable<IPickupCount> PickupSummary { get; set; }
        void AddPickupCount(IPickupCount p);
        string Reference { get; set; }
        PackageLocation PackageLocation { get; set; }
        string SpecialInstructions { get; set; }
        DateTime PickupDate { get; set; }
        string PickupConfirmationNumber { get; set; }
        string PickupId { get; set; }
    }
}
