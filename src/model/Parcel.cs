
namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Parcel : IParcel
    {
        virtual public IParcelDimension Dimension { get; set; }
        virtual public IParcelWeight Weight { get; set; }
        virtual public decimal ValueOfGoods { get;set;}
        virtual public string CurrencyCode {get;set;}
    }
}