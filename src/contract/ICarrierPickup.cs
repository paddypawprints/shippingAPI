using System;
using System.Collections.Generic;
using System.Text;


namespace PitneyBowes.Developer.ShippingApi
{
    public interface ICarrierPickup
    {
        IAddress PickupAddress { get; set; }
        Carrier Carrier { get; set; }
        IEnumerable<IPickupCount> PickupSummary { get; set; }
        string Required { get; set; }
        PackageLocation PackageLocation { get; set; }
        string SpecialInstructions { get; set; }
        //public bool ShouldSerializePickupDateTime() => false;
        DateTimeOffset PickupDateTime { get; set; }
        //public bool ShouldSerializePickupConfirmationNumber() => false;
        string PickupConfirmationNumber { get; set; }
        // public bool ShouldSerializePickupId() => false;
        string PickupId { get; set; }
    }
}


