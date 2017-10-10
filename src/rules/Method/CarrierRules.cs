using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi.Rules
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

        public async static Task<ShippingApiResponse<CarrierRule>> RatingServices(RatingServicesRequest request, ISession session = null) 
        {
            var response =  await WebMethod.Get<ServiceRule[], RatingServicesRequest>("/shippingservices/v1/information/rules/rating-services", request, session);
            var carrierRuleResponse = new ShippingApiResponse<CarrierRule>();
            carrierRuleResponse.Errors = response.Errors;
            carrierRuleResponse.HttpStatus = response.HttpStatus;
            carrierRuleResponse.Success = response.Success;
            if (response.Success)
            {
                carrierRuleResponse.APIResponse = new CarrierRule()
                {
                    Carrier = request.Carrier,
                    DestinationCountry = request.DestinationCountryCode,
                    OriginCountry = request.OriginCountryCode,
                    ServiceRules = new IndexedList<Services, ServiceRule>()
                };
                foreach (var s in response.APIResponse)
                {
                    carrierRuleResponse.APIResponse.ServiceRules.Add(s.ServiceId, s);
                }
            }
            return carrierRuleResponse;

        }
    }

}
  