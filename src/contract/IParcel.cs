namespace PitneyBowes.Developer.ShippingApi
{
    public interface IParcel
    {
        IParcelDimension Dimension { get; set; }
        IParcelWeight Weight { get; set; }
        decimal ValueOfGoods { get; set; }
        string CurrencyCode { get; set; }
    }
}