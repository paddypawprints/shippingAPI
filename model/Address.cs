
using Newtonsoft.Json;
using System.Collections.Generic;

namespace com.pb.shippingapi.model
{
    public class Address
    {
        [JsonProperty("addressLines")]
        public IEnumerable<string> AddressLines { get; set;}
        [JsonProperty("cityTown")]
        public string CityTown { get; set;}
        [JsonProperty("stateProvince")]
        public string StateProvince { get;set;}
        [JsonProperty("postalCode")]
        public string PostalCode {get;set;}
        [JsonProperty("countryCode")]
        public string CountryCode {get;set;}
        [JsonProperty("company")]
        public string Company {get;set;}
        [JsonProperty("name")]
        public string Name {get;set;}
        [JsonProperty("phone")]
        public string Phone { get;set;}
        [JsonProperty("email")]
        public string Email {get;set;}
        [JsonProperty("residential")]
        public bool Residential {get;set;}
        [JsonProperty("status")]
        public AddressStatus Status { get;set;}
    }
}