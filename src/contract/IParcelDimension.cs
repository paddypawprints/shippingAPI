namespace PitneyBowes.Developer.ShippingApi
{
    public interface IParcelDimension
    {
        decimal Length { get; set; }
        decimal Height { get; set; }
        decimal Width { get; set; }
        UnitOfDimension UnitOfMeasurement { get; set; }
        decimal IrregularParcelGirth { get; set; }
    }
}