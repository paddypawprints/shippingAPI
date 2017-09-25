using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi.Method
{
    public class RatingServicesRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization {get;set;}

        [ShippingApiQuery("carrier")]
        public Carrier Carrier { get; set; }
        [ShippingApiQuery("originCountryCode")]
        public string OriginCountryCode { get; set; }
        [ShippingApiQuery("destinationCountryCode")]
        public string DestinationCountryCode { get; set; }
    }

    public static class CarrierRulesMethods
    {

        public async static Task<ShippingApiResponse<T>> RatingServices<T>(RatingServicesRequest request, Session session = null) where T : IEnumerable<ICarrierRule>
        {
            if (session == null) session = SessionDefaults.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            var response =  await WebMethod.Get<T, RatingServicesRequest>("/shippingservices/v1/information/rules/rating-services", request, session);
            if (response.Success)
            {
                foreach ( var rule in response.APIResponse )
                {
                    rule.Carrier = request.Carrier;
                }
            }
            return response;
        }
    }

}
  