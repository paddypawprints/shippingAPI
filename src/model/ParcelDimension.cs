
namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class ParcelDimension : IParcelDimension
    {
        virtual public decimal Length { get; set;}

        virtual public decimal Height { get; set;}

        virtual public decimal Width { get;set;}

        virtual public UnitOfDimension UnitOfMeasurement {get;set;}

        virtual public decimal IrregularParcelGirth {get;set;}

    }
}