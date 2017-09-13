


namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    /// <summary>
    /// Street address and/or apartment and/or P.O. Box. You can specify up to three address lines.
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

        private AddressFluent()
        {
            
        }
        public AddressFluent<T> AddressLines(string a1, string a2 = null, string a3 = null)
        {
            _address.AddAddressLine(a1);
            if (a2 != null) _address.AddAddressLine(a2);
            if (a3 != null) _address.AddAddressLine(a3);
            return this;
        }

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
            var addressResponse = AddressessMethods.VerifyAddress<T>(_address).GetAwaiter().GetResult();
            if (addressResponse.Success)
            {
                _address = addressResponse.APIResponse;
            }
            return this;
        }
    }
}