
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Rules;
using Xunit;

namespace tests
{
    public class CarrierRulesTests
    {
        [Fact]
        void ParseCarrierRules()
        {
            TestSession.Initialize(true);

            var req = new RatingServicesRequest()
            {

                Carrier = Carrier.USPS,
                OriginCountryCode = "US",
                DestinationCountryCode = "US"
            };
            var res = CarrierRulesMethods.RatingServices(req).GetAwaiter().GetResult();

        }

    }
}
