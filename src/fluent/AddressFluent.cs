using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Method;


namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    /// <summary>
    /// An address. If part of a response, this object also specifies address validation status, unless minimum validation is enabled.
    /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-address"/>
    /// </summary>
    public class AddressFluent<T> where T : IAddress, new()
    {
        private T _address;

        public static implicit operator T( AddressFluent<T> a)
        {
            return a._address;
        }

        public static AddressFluent<T> Create()
        {
            var a = new AddressFluent<T>()
            {
                _address = new T()
            };
            return a;
        }

        public AddressFluent()
        {
            _address = new T();
        }

        public AddressFluent(IAddress a )
        {
            _address = (T)a;
        }

        /// <summary>
        /// Specify up to three address lines for the street address, including suite number.
        /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-address"/>
        /// </summary>
        public AddressFluent<T> AddressLines(string a1, string a2 = null, string a3 = null)
        {
            _address.AddAddressLine(a1);
            if (a2 != null) _address.AddAddressLine(a2);
            if (a3 != null) _address.AddAddressLine(a3);
            return this;
        }

        /// <summary>
        /// City of town.
        /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-address"/>
        /// </summary>
        public AddressFluent<T> CityTown(string c) 
        {
            _address.CityTown = c;
            return this;
        }
        public AddressFluent<T> StateProvince(string s) 
        { 
            _address.StateProvince = s;
            return this;
        }

        public AddressFluent<T> PostalCode( string s) 
        {
            _address.PostalCode = s;
            return this;
        }
        /// <summary>
        /// Two character country code. The country code is required for all shipments.
        /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-address"/>
        /// </summary>
        public AddressFluent<T> CountryCode(string s) 
        {
            _address.CountryCode = s;
            return this;
        }
        public AddressFluent<T> Company(string s) 
        {
            _address.Company = s;
            return this;
        }

        public AddressFluent<T> Name(string s) 
        {
            _address.Name=s;
            return this;
        }

        public AddressFluent<T> Phone(string s) 
        { 
            _address.Phone = s;
            return this;
        }

        public AddressFluent<T> Email(string s) 
        {
            _address.Email = s;
            return this;
        }

        public AddressFluent<T> Residential(bool b) 
        {
            _address.Residential = b;
            return this;
        }

        public  AddressFluent<T> Status(AddressStatus s) 
        { 
            _address.Status = s;
            return this;
        }

        public AddressFluent<T> Verify()
        {
            var addressResponse = AddressessMethods.VerifyAddress(_address).GetAwaiter().GetResult();
            if (addressResponse.Success)
            {
                _address = addressResponse.APIResponse;
            }
            return this;
        }
        public IEnumerable<AddressFluent<T>> VerifySuggest()
        {
            var addressResponse = AddressessMethods.VerifySuggestAddress<T>(_address).GetAwaiter().GetResult();
            if (addressResponse.Success)
            {
                _address = (T)addressResponse.APIResponse.Address;
                foreach( var a in addressResponse.APIResponse.Suggestions.Addresses)
                {
                    yield return new AddressFluent<T>((T)a);
                }
            }
            yield break;
        }
    }
}