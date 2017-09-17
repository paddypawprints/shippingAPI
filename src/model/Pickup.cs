using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Pickup : IPickup
    {
        virtual public IAddress PickupAddress{get; set;}
        virtual public Carrier Carrier{get; set;}
        virtual public IEnumerable<IPickupCount> PickupSummary{get; set;}
        virtual public string Reference{get; set;}
        virtual public PackageLocation PackageLocation{get; set;}
        virtual public string SpecialInstructions{get; set;}
        virtual public DateTime PickupDate { get; set; }
        virtual public string PickupConfirmationNumber { get; set; }
        virtual public string PickupId { get; set; }
    }
}
