using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi.Method
{

    public static class RatesMethods
    {
        public async static Task<ShippingApiResponse<T>> Rates<T>(T request, Session session = null) where T : IRates, new()
        {
            var ratesRequest = new JsonRates<T>(request);
            if (session == null) session = SessionDefaults.DefaultSession;
            ratesRequest.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<T, JsonRates<T>>("/shippingservices/v1/rates", ratesRequest, session);
        }

    }
}
