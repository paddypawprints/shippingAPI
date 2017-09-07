namespace PitneyBowes.Developer.ShippingApi
{
    public interface ICustomsItems
    {
        string Description { get; set; }
        int Quantity { get; set; }
        decimal UnitPrice { get; set; }
        IParcelWeight UnitWeight { get; set; }
        string HSTariffCode { get; set; }
        string OriginCountryCode { get; set; }
    }

}