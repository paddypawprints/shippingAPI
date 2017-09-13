
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace PitneyBowes.Developer.ShippingApi
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AddressVerifyRequest<T> : IShippingApiRequest where T: IAddress, new()
    {
        public AddressVerifyRequest()
        {
            Wrapped = new T();
            Suggest = null;
        }
        public AddressVerifyRequest(T t)
        {
            Wrapped = t;
            Suggest = null;
        }

        public IAddress Wrapped { get; set; }

        [ShippingAPIQuery("returnSuggestions", true)]
        public bool? Suggest { get; set; }

        [JsonProperty("addressLines")]
        public IEnumerable<string> AddressLines
        {
            get => Wrapped.AddressLines;
            set { Wrapped.AddressLines = value; }
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
            set { Wrapped.PostalCode = value; }
        }
        [JsonProperty("countryCode")]
        public string CountryCode
        {
            get => Wrapped.CountryCode;
            set { Wrapped.CountryCode = value; }
        }
        [JsonProperty("company")]
        public string Company
        {
            get => Wrapped.Company;
            set { Wrapped.Company = value; }
        }
        [JsonProperty("name")]
        public string Name
        {
            get => Wrapped.Name;
            set { Wrapped.Name = value; }
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

        public string ContentType { get => "application/json"; set => throw new NotImplementedException(); }
        
        [ShippingAPIHeader("Bearer")]
        public StringBuilder Authorization { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class AddressSuggestions
    {
        [JsonProperty("suggestionType")]
        string SuggestionType { get; set; } //TODO - figure out possible values and convert to enum
        [JsonProperty("address")]
        IEnumerable<IAddress> Addresses { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class VerifySuggestResponse
    {
        [JsonProperty("address")]
        IAddress Address { get; set; }
        [JsonProperty("suggestions")]
        AddressSuggestions Suggestions { get; set; }
    }

    public static class AddressessMethods
    {
        public async static Task<ShippingAPIResponse<T>> VerifyAddress<T>(T request, ShippingApi.Session session = null) where T : IAddress, new()
        {
            var verifyRequest = new AddressVerifyRequest<T>(request);
            if (session == null) session = ShippingApi.DefaultSession;
            verifyRequest.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<T, AddressVerifyRequest<T>>("/shippingservices/v1/addresses/verify", verifyRequest, session);
        }
        public async static Task<ShippingAPIResponse<AddressSuggestions>> VerifySuggestAddress<T>(AddressVerifyRequest<T> request, ShippingApi.Session session = null) where T : IAddress, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            request.Suggest = true;
            return await WebMethod.Post<AddressSuggestions, AddressVerifyRequest<T>>("/shippingservices/v1/addresses/verify-suggest", request, session);
        }
    }
}
