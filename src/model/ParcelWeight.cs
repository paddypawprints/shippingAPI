

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ParcelWeight : IParcelWeight
    {
        virtual public decimal Weight { get; set;}
        virtual public UnitOfWeight UnitOfMeasurement { get; set;}
    }
}