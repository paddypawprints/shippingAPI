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

using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    /// <summary>
    /// Street address and/or apartment and/or P.O. Box. You can specify up to three address lines.
    /// </summary>
    public class  Address : IAddress
    {
        
        public Address() => Status = AddressStatus.NOT_CHANGED;
        /// <summary>
        /// Street address and/or apartment and/or P.O. Box. You can specify up to
        ///three address lines.
        ///
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
        virtual public IEnumerable<string> AddressLines { get; set; }
        /// <summary>
        /// Adds the address line.
        /// </summary>
        /// <param name="s">Line to add.</param>
        virtual public void AddAddressLine(string s)
        {
            if (AddressLines as List<string> != null )
            {
                if ((AddressLines as List<string>).Count == 3) throw new InvalidOperationException("Address can only have 3 lines");
            }
            ModelHelper.AddToEnumerable<string, string>(s,()=>AddressLines, (x)=>AddressLines = x);
        }
        /// <summary>
        /// Gets or sets the city town.
        /// </summary>
        /// <value>The city town.</value>
        virtual public string CityTown { get; set;}
        /// <summary>
        /// Gets or sets the state province.  For US address, use the 2-letter state code.
        /// </summary>
        /// <value>The state province.</value>
        virtual public string StateProvince { get;set;}
        /// <summary>
        /// Gets or sets the postal code.  For US addresses, either the 5-digit or 9-digit zip code.
        /// </summary>
        /// <value>The postal code.</value>
        virtual public string PostalCode {get;set;}
        /// <summary>
        /// Gets or sets the country code. Two-character country code from the ISO country list.
        /// </summary>
        /// <value>The country code.</value>
        virtual public string CountryCode {get;set;}
        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        virtual public string Company {get;set;}
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        virtual public string Name {get;set;}
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        virtual public string Phone { get;set;}
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        virtual public string Email {get;set;}
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PitneyBowes.Developer.ShippingApi.Model.Address"/>
        /// is residential. Indicates whether this is a residential address. It is recommended that
        /// this parameter be passed in as the address verification process is more
        /// accurate with it.
        /// </summary>
        /// <value><c>true</c> if residential; otherwise, <c>false</c>.</value>
        virtual public bool Residential {get;set;}
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PitneyBowes.Developer.ShippingApi.Model.Address"/> class. The response returns this field only if
        /// ``MINIMAL_ADDRESS_VALIDATION`` is **NOT** enabled.
        ///
        ///This indicates whether any action has been performed on the address during cleansing.
        /// </summary>
        virtual public AddressStatus Status { get;set;}
    }
}