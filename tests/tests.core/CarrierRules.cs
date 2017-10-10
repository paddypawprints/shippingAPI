
/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

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
