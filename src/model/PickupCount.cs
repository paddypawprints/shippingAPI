using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class PickupCount : IPickupCount
    {
        virtual public Services ServiceId{get; set;}
        virtual public int Count{get; set;}
        virtual public IParcelWeight TotalWeight{get; set;}
    }
}
