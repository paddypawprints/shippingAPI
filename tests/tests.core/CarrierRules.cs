using System;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Method;
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
            var res = CarrierRulesMethods.RatingServices<CarrierRule[]>(req).GetAwaiter().GetResult();

        }

    }
}
