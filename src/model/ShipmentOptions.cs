

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ShipmentOptions : IShipmentOptions
    {
        virtual public ShipmentOption ShipmentOption { get; set;}
        virtual public string Value { get;set;}
    }

}