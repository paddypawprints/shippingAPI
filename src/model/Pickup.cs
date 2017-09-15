using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Pickup : IPickup
    {

        public IAddress PickupAddress{get; set;}
        public Carrier Carrier{get; set;}
        public IEnumerable<IPickupCount> PickupSummary{get; set;}
        public string Reference{get; set;}
        public PackageLocation PackageLocation{get; set;}
        public string SpecialInstructions{get; set;}
        public DateTime PickupDate { get; set; }
        public string PickupConfirmationNumber { get; set; }
        public string PickupId { get; set; }
    }
}
