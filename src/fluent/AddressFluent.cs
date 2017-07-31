
using Newtonsoft.Json;
using System.Collections.Generic;
using com.pb.shippingapi.model;


namespace com.pb.shippingapi.fluent
{
    /// <summary>
    /// Street address and/or apartment and/or P.O. Box. You can specify up to three address lines.
    /// </summary>
    public class AddressFluent 
    {
        private Address _address;

        public static implicit operator Address( AddressFluent a)
        {
            return a._address;
        }

        public static AddressFluent Create()
        {
            return new AddressFluent();
        }
        public AddressFluent()
        {
            _address = new Address();
        }
        public AddressFluent AddressLines(string a1, string a2 = null, string a3 = null)
        {
            var addressLines = new List<string>();
            addressLines.Add(a1);
            if (a2 != null) addressLines.Add(a2);
            if (a3 != null) addressLines.Add(a3);
            _address.AddressLines = addressLines;
            return this;
        }

        public AddressFluent CityTown(string c) 
        {
            _address.CityTown = c;
            return this;
        }
        public AddressFluent StateProvince(string s) 
        { 
            _address.StateProvince = s;
            return this;
        }

        public AddressFluent PostalCode( string s) 
        {
            _address.PostalCode = s;
            return this;
        }
        public AddressFluent CountryCode(string s) 
        {
            _address.CountryCode = s;
            return this;
        }
        public AddressFluent Company(string s) 
        {
            _address.Company = s;
            return this;
        }

        public AddressFluent Name(string s) 
        {
            _address.Name=s;
            return this;
        }

        public AddressFluent Phone(string s) 
        { 
            _address.Phone = s;
            return this;
        }

        public AddressFluent Email(string s) 
        {
            _address.Email = s;
            return this;
        }

        public AddressFluent Residential(bool b) 
        {
            _address.Residential = b;
            return this;
        }

        public  AddressFluent Status(AddressStatus s) 
        { 
            _address.Status = s;
            return this;
        }
    }
}