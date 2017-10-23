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

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;


namespace PitneyBowes.Developer.ShippingApi.Method
{

    /// <summary>
    /// Address suggestions.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AddressSuggestions
    {
        /// <summary>
        /// The part of the address that was changed from the original.
        /// </summary>
        [JsonProperty("suggestionType")]
        public string SuggestionType { get; set; } //TODO - figure out possible values and convert to enum
        /// <summary>
        /// Each address object provides an alternative suggested address.
        /// </summary>
        /// <value>The addresses.</value>
        [JsonProperty("address")]
        public IEnumerable<IAddress> Addresses { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class VerifySuggestResponse
    {
        [JsonProperty("address")]
        public IAddress Address { get; set; }
        [JsonProperty("suggestions")]
        public AddressSuggestions Suggestions { get; set; }
    }

    public static class AddressessMethods
    {
        /// <summary>
        /// Address validation verifies and cleanses postal addresses within the United
        /// States to help ensure packages are rated accurately and shipments arrive at
        /// their final destinations on time.This API call sends an address to be
        /// verified.The response returns a valid address.
        /// 1. Only U.S. domestic addresses are validated using this POST operation at this time.
        ///
        /// 2. Addresses are validated by the Pitney Bowes Address Validation (AV) engine.
        ///
        /// 3. Complete addresses, including address line(s) and city/state/zip are always
        /// validated by default. An error is returned if validation fails.
        /// - When the ``minimalAddressValidation`` query parameter flag is set to true,
        ///  the address line(s) are left as is and are not formatted, re-arranged or
        ///  included in the validation check.Only the city/state/zip line is
        ///  checked for validity.
        ///  ** Note:** When using this option, please ensure that street addresses on
        ///   the labels are actually deliverable, as street addresses are not
        ///   validated by the Pitney Bowes AV engine.
        /// - When the ``minimalAddressValidation`` query parameter flag is set to false (by
        ///   default), the complete address, address line(s) and city/state/zip line
        ///   are included in the validation check.
        ///
        /// 4. Address validation returns 2-digit delivery points when available. You can find
        /// more information on delivery points at the |delivery-points.
        /// <a href="https://en.wikipedia.org/wiki/Delivery_point" target="_blank">Wikipedia Delivery Point page</a>
        /// 
        /// 5. If validation fails, you can use the :doc:`/api/post-address-suggest` API call
        /// to provide suggestions that could result in the address passing
        /// verification in a subsequent Address Validation API call.
        /// </summary>
        /// <returns>The address.</returns>
        /// <param name="request">Request.</param>
        /// <param name="session">Session.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async static Task<ShippingApiResponse<T>> VerifyAddress<T>(T request, ISession session = null) where T : IAddress, new()
        {
            var verifyRequest = new JsonAddress<T>(request);
            return await WebMethod.Post<T, JsonAddress<T>>("/shippingservices/v1/addresses/verify", verifyRequest, session);
        }
        /// <summary>
        /// Verifies the suggest address. This POST operation obtains suggested addresses in cases where the
        /// Address Validation API call has returned an error.
        /// 1. The suggested addresses are **not** validated addresses. You must reissue
        /// the API call.
        ///
        /// 2. Some suggestions might not be valid delivery points. This is especially
        /// true for range suggestions. For example, if the suggested valid range for a
        /// street is 1-100, the Suggest Addresses API call will consider all numbers 
        /// the range to be valid, even if the only valid delivery points are
        /// numbers 12, 24, and 36.
        ///
        /// 3. The operation provides several types of suggestions:
        /// - Range suggestions:
        /// - Primary number range (e.g., street number, PO Box number)
        /// - Secondary number range(e.g., suite number, apartment number
        /// - Street Name
        /// - City Name
        /// - Company Name
        /// - Puerto Rican Urbanization
        ///
        /// 4. The suggested addresses are **not** sorted by best match.
        ///
        /// 5. The API returns a maximum of 20 suggestions.
        ///
        /// 6. Some addresses might return no suggestions. If there are no
        /// suggestions, the ``suggestions`` object is not returned.
        /// </summary>
        /// <returns>The suggest address.</returns>
        /// <param name="request">Request.</param>
        /// <param name="session">Session.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async static Task<ShippingApiResponse<VerifySuggestResponse>> VerifySuggestAddress<T>(T request, ISession session = null) where T : IAddress, new()
        {
            var verifyRequest = new JsonAddress<T>(request) { Suggest = true };
            return await WebMethod.Post<VerifySuggestResponse, JsonAddress<T>>("/shippingservices/v1/addresses/verify-suggest", verifyRequest, session);
        }
    }
}
