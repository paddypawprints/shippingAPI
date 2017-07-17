
using Newtonsoft.Json;
using System.Collections.Generic;


namespace com.pb.shippingapi.model
{
    /// <summary>
    /// Street address and/or apartment and/or P.O. Box. You can specify up to three address lines.
    /// </summary>
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