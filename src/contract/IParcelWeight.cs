namespace PitneyBowes.Developer.ShippingApi
{
    public interface IParcelWeight
    {
        decimal Weight { get; set; }
        UnitOfWeight UnitOfMeasurement { get; set; }
    }
}