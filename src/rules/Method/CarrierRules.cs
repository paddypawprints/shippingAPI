/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System.Text;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public class RatingServicesRequest : ShippingApiRequest
    {
        public override string ContentType => "application/json";

        [ShippingApiHeader("Bearer")]
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
            var carrierRuleResponse = new ShippingApiResponse<CarrierRule>
            {
                Errors = response.Errors,
                HttpStatus = response.HttpStatus,
                Success = response.Success
            };
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
  