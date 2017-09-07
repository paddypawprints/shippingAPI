namespace PitneyBowes.Developer.ShippingApi
{
    public interface IShipmentOptions
    {
        ShipmentOption ShipmentOption { get; set; }
        string Value { get; set; }
    }
}