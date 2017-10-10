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
        public async static Task<ShippingApiResponse<T>> Rates<T>(T request, ISession session = null) where T : IShipment, new()
        {
            var ratesRequest = new JsonShipment<T>(request);
            return await WebMethod.Post<T, JsonShipment<T>>("/shippingservices/v1/rates", ratesRequest, session);
        }
    }
}
