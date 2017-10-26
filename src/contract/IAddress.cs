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

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// An address. If part of a response, this object also specifies address validation status, unless minimum validation is enabled.
    /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-address"/>
    /// </summary>

    public interface IAddress
    {
        /// <summary>
        /// Street address and/or apartment and/or P.O. Box. You can specify up to
        /// three address lines.
        /// For USPS domestic destinations, ensure that the street address is
        /// specified as the last of the 3 address lines.This way, the street
        /// address is printed right above the city, state, postal zip code, per
        /// USPS label guidelines.
        /// 
        /// See <a href="https://shipping.pitneybowes.com/api/post-shipments.html"/> for considerations when specifying
        /// multiple lines in a shipment's ``fromAddress`` when
        /// ``MINIMAL_ADDRESS_VALIDATION`` is enabled.
        /// </summary>
        /// <value>The address lines.</value>
        IEnumerable<string> AddressLines { get; set; }
        void AddAddressLine( string s);
        /// <summary>
        /// Gets or sets the city town.
        /// </summary>
        /// <value>The city town.</value>
        string CityTown { get; set; }
        /// <summary>
        /// Gets or sets the state province. For US address, use the 2-letter state code.
        /// </summary>
        /// <value>The state province.</value>
        string StateProvince { get; set; }
        /// <summary>
        /// Gets or sets the postal code. Two-character country code from the ISO country list.
        /// </summary>
        /// <value>The postal code.</value>
        string PostalCode { get; set; }
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        string CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        string Company { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        string Phone { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        string Email { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PitneyBowes.Developer.ShippingApi.IAddress"/> is residential. Indicates whether this is a residential address. It is recommended that
        /// this parameter be passed in as the address verification process is more accurate with it.
        /// </summary>
        /// <value><c>true</c> if residential; otherwise, <c>false</c>.</value>
        bool Residential { get; set; }
        /// <summary>
        /// The response returns this field only if
        /// ``MINIMAL_ADDRESS_VALIDATION`` is **NOT** enabled
        /// This indicates whether any action has been performed on the address during cleansing.
        /// </summary>
        /// <value>The status.</value>
        AddressStatus Status { get; set; }
    }

    public static partial class InterfaceExtensions
    {
        public static bool IsValid(this IAddress a) => true;
    }

}