using System;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    /// <summary>
    /// Street address and/or apartment and/or P.O. Box. You can specify up to three address lines.
    /// </summary>
   
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonAddress<T> : JsonWrapper<T>, IAddress, IShippingApiRequest where T : IAddress, new()
    {
        public JsonAddress() : base() {}

        public JsonAddress(T t) : base(t) {}

        [ShippingApiQuery("returnSuggestions", true)]
        public bool? Suggest { get; set; }

        [JsonProperty("addressLines", Order = 5)]
        public IEnumerable<string> AddressLines
        { 
            get => Wrapped.AddressLines;
            set { Wrapped.AddressLines = value;}
        }
        public void AddAddressLine(string s)
        {
            Wrapped.AddAddressLine(s);
        }
        [JsonProperty("cityTown", Order = 6)]
        public string CityTown
        {
            get => Wrapped.CityTown;
            set { Wrapped.CityTown = value; }
        }
        [JsonProperty("stateProvince", Order = 7)]
        public string StateProvince
        {
            get => Wrapped.StateProvince;
            set { Wrapped.StateProvince = value; }
        }
        [JsonProperty("postalCode", Order = 8)]
        public string PostalCode
        {
            get => Wrapped.PostalCode;
            set { Wrapped.PostalCode = value;  }
        }
        [JsonProperty("countryCode", Order = 9)]
        public string CountryCode
        {
            get => Wrapped.CountryCode;
            set { Wrapped.CountryCode = value;  }
        }
        [JsonProperty("company",Order =0)]
        public string Company
        {
            get => Wrapped.Company;
            set { Wrapped.Company = value;  }
        }
        [JsonProperty("name", Order =1)]
        public string Name
        {
            get => Wrapped.Name;
            set { Wrapped.Name = value;  }
        }
        [JsonProperty("phone", Order = 2)]
        public string Phone
        {
            get => Wrapped.Phone;
            set { Wrapped.Phone = value; }
        }
        [JsonProperty("email", Order = 3)]
        public string Email
        {
            get => Wrapped.Email;
            set { Wrapped.Email = value; }
        }
        [JsonProperty("residential", Order = 4)]
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

        public string ContentType { get => "application/json"; }

        [ShippingApiHeaderAttribute("Bearer")]
        public StringBuilder Authorization { get; set; }

        public string GetUri(string baseUrl)
        {
            StringBuilder uri = new StringBuilder(baseUrl);
            ShippingApiRequest.AddRequestResource(this, uri);
            ShippingApiRequest.AddRequestQuery(this, uri);
            return uri.ToString();
        }

        public IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        public void SerializeBody(StreamWriter writer, ShippingApi.Session session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }

    }
}