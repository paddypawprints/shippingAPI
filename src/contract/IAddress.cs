using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// An address. If part of a response, this object also specifies address validation status, unless minimum validation is enabled.
    /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-address"/>
    /// </summary>

    public interface IAddress
    {
        IEnumerable<string> AddressLines { get; set; }
        void AddAddressLine( string s);
        string CityTown { get; set; }
        string StateProvince { get; set; }
        string PostalCode { get; set; }
        string CountryCode { get; set; }
        string Company { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        bool Residential { get; set; }
        AddressStatus Status { get; set; }
    }
}