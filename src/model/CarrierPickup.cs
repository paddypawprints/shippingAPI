using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CarrierPickup : ICarrierPickup
    {

        public IAddress PickupAddress{get; set;}
        public Carrier Carrier{get; set;}
        public IEnumerable<IPickupCount>PickupSummary{get; set;}
        public string Required{get; set;}
        public PackageLocation PackageLocation{get; set;}
        public string SpecialInstructions{get; set;}
        public DateTimeOffset PickupDateTime{get; set;}
        public string PickupConfirmationNumber{get; set;}
        public string PickupId{get; set;}
    }
}
