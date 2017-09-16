using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class PickupCount : IPickupCount
    {

        public Services ServiceId{get; set;}
        public int Count{get; set;}
        public IParcelWeight TotalWeight{get; set;}
    }
}
