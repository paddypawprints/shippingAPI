
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace PitneyBowes.Developer.ShippingApi
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CountriesRequest<T> : IShippingApiRequest where T : ICountry, new()
    {
        [ShippingAPIQuery("carrier")]
        public Carrier Carrier { get; set; }

        [ShippingAPIQuery("originCountryCode")]
        public string OriginCountryCode { get; set; }

        public string ContentType { get => "application/json"; set => throw new NotImplementedException(); }

        [ShippingAPIHeader("Bearer")]
        public StringBuilder Authorization { get; set; }
    }

    public static class CountriesMethods
    {

        public async static Task<ShippingAPIResponse<IEnumerable<T>>> Countries<T>(CountriesRequest<T> request, ShippingApi.Session session = null) where T : ICountry, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<IEnumerable<T>, CountriesRequest<T>>("/shippingservices/v1/countries", request, session);
        }

    }

}