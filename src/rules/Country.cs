


namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Country : ICountry
    {
        virtual public string CountryCode { get; set; }
        virtual public string CountryName { get; set; }
    }
}