using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi
{
    public class RatingServicesRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization {get;set;}

        [ShippingAPIQuery("carrier")]
        Carrier Carrier { get; set; }
        [ShippingAPIQuery("originCountryCode")]
        string OriginCountryCode { get; set; }
        [ShippingAPIQuery("destinationCountryCode")]
        string DestinationCountryCode { get; set; }
    }

    public static class CarrierRules
    {

        public async static Task<ShippingAPIResponse<T>> RatingServices<T>(RatingServicesRequest request, ShippingApi.Session session = null) where T : ICarrierRule, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<T, RatingServicesRequest>("/shippingservices/v1/countries", request, session);
        }
    }

}
  