namespace PitneyBowes.Developer.ShippingApi
{
    public interface  IPickupCount
    {
        Services ServiceId { get; set; }
        int Count { get; set; }
        IParcelWeight TotalWeight { get; set; }
    }
}


