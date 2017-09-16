using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi
{
    public class RatingServicesRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization {get;set;}

        [ShippingApiQuery("carrier")]
        Carrier Carrier { get; set; }
        [ShippingApiQuery("originCountryCode")]
        string OriginCountryCode { get; set; }
        [ShippingApiQuery("destinationCountryCode")]
        string DestinationCountryCode { get; set; }
    }

    public static class CarrierRules
    {

        public async static Task<ShippingApiResponse<T>> RatingServices<T>(RatingServicesRequest request, ShippingApi.Session session = null) where T : ICarrierRule, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<T, RatingServicesRequest>("/v1/information/rules/rating-services", request, session);
        }
    }

}
  