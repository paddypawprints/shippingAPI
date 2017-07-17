
using Newtonsoft.Json;
using System.Collections.Generic;
using com.pb.shippingapi;


namespace com.pb.shippingapi.fluent
{
    /// <summary>
    /// Street address and/or apartment and/or P.O. Box. You can specify up to three address lines.
    /// </summary>
    public class Address
    {
        private model.Address _address;

        public static implicit operator model.Address( Address a)
        {
            return a._address;
        }

        public static Address Create()
        {
            return new Address();
        }
        public Address()
        {
            _address = new model.Address();
        }
        public Address AddressLines(string a1, string a2 = null, string a3 = null)
        {
            var addressLines = new List<string>();
            addressLines.Add(a1);
            if (a2 != null) addressLines.Add(a2);
            if (a3 != null) addressLines.Add(a3);
            _address.AddressLines = addressLines;
            return this;
        }

        public Address CityTown(string c) 
        {
            _address.CityTown = c;
            return this;
        }
        public Address StateProvince(string s) 
        { 
            _address.StateProvince = s;
            return this;
        }

        public Address PostalCode( string s) 
        {
            _address.PostalCode = s;
            return this;
        }
        public Address CountryCode(string s) 
        {
            _address.CountryCode = s;
            return this;
        }
        public Address Company(string s) 
        {
            _address.Company = s;
            return this;
        }

        public Address Name(string s) 
        {
            _address.Name=s;
            return this;
        }

        public Address Phone(string s) 
        { 
            _address.Phone = s;
            return this;
        }

        public Address Email(string s) 
        {
            _address.Email = s;
            return this;
        }

        public Address Residential(bool b) 
        {
            _address.Residential = b;
            return this;
        }

        public  Address Status(model.AddressStatus s) 
        { 
            _address.Status = s;
            return this;
        }
    }
}