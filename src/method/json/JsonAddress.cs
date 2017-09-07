using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    /// <summary>
    /// Street address and/or apartment and/or P.O. Box. You can specify up to three address lines.
    /// </summary>
   
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonAddress<T> : JsonWrapper<T>, IAddress where T : IAddress, new()
    {
        public JsonAddress() : base() {}

        public JsonAddress(T t) : base(t) {}

        [JsonProperty("addressLines")]
        public IEnumerable<string> AddressLines
        { 
            get => Wrapped.AddressLines;
            set { Wrapped.AddressLines = value;}
        }
        public void AddAddressLine(string s)
        {
            Wrapped.AddAddressLine(s);
        }
        [JsonProperty("cityTown")]
        public string CityTown
        {
            get => Wrapped.CityTown;
            set { Wrapped.CityTown = value; }
        }
        [JsonProperty("stateProvince")]
        public string StateProvince
        {
            get => Wrapped.StateProvince;
            set { Wrapped.StateProvince = value; }
        }
        [JsonProperty("postalCode")]
        public string PostalCode
        {
            get => Wrapped.PostalCode;
            set { Wrapped.PostalCode = value;  }
        }
        [JsonProperty("countryCode")]
        public string CountryCode
        {
            get => Wrapped.CountryCode;
            set { Wrapped.CountryCode = value;  }
        }
        [JsonProperty("company")]
        public string Company
        {
            get => Wrapped.Company;
            set { Wrapped.Company = value;  }
        }
        [JsonProperty("name")]
        public string Name
        {
            get => Wrapped.Name;
            set { Wrapped.Name = value;  }
        }
        [JsonProperty("phone")]
        public string Phone
        {
            get => Wrapped.Phone;
            set { Wrapped.Phone = value; }
        }
        [JsonProperty("email")]
        public string Email
        {
            get => Wrapped.Email;
            set { Wrapped.Email = value; }
        }
        public bool ShouldSerializeResidential() => false;
        [JsonProperty("residential")]
        public bool Residential
        {
            get => Wrapped.Residential;
            set { Wrapped.Residential = value; }
        }
        public bool ShouldSerializeStatus() => false;
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AddressStatus Status
        {
            get => Wrapped.Status;
            set { Wrapped.Status = value; }
        }
    }
}