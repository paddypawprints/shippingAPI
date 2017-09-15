﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
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

        public string GetUri(string baseUrl)
        {
            StringBuilder uri = new StringBuilder(baseUrl);
            ShippingApiRequest.AddRequestResource(this, uri);
            ShippingApiRequest.AddRequestQuery(this, uri);
            return uri.ToString();
        }

        public IEnumerable<Tuple<ShippingAPIHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        public void SerializeBody(StreamWriter writer, ShippingApi.Session session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }

        [JsonProperty("destinationZone")]

        public string ContentType => "application/json";

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