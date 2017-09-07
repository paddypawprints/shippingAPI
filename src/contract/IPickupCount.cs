namespace PitneyBowes.Developer.ShippingApi
{
    public interface  IPickupCount
    {
        USPSServices ServiceId { get; set; }
        int Count { get; set; }
        IParcelWeight TotalWeight { get; set; }
    }
}


