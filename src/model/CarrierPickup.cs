using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CarrierPickup : ICarrierPickup
    {
        virtual public IAddress PickupAddress{get; set;}
        virtual public Carrier Carrier{get; set;}
        virtual public IEnumerable<IPickupCount>PickupSummary{get; set;}
        virtual public string Required{get; set;}
        virtual public PackageLocation PackageLocation{get; set;}
        virtual public string SpecialInstructions{get; set;}
        virtual public DateTimeOffset PickupDateTime{get; set;}
        virtual public string PickupConfirmationNumber{get; set;}
        virtual public string PickupId{get; set;}
    }
}
