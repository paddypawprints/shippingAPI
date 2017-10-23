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

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CustomsItems : ICustomsItems
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        virtual public string Description { get; set;}
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        virtual public int Quantity { get; set;}
        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>The unit price.</value>
        virtual public decimal UnitPrice { get;set;}
        /// <summary>
        /// Gets or sets the unit weight.
        /// </summary>
        /// <value>The unit weight.</value>
        virtual public IParcelWeight UnitWeight {get;set;}
        /// <summary>
        /// Harmonized Tariff associated with the commodity.
        /// </summary>
        /// <value>Harmonized Tariff associated with the commodity.code.</value>
        virtual public string HSTariffCode {get;set;}
        /// <summary>
        /// The county code of the shipment origin. Use ISO 3166-1 alpha-2 standard values.
        /// </summary>
        /// <value>The origin country code.</value>
        virtual public string OriginCountryCode {get;set;}
    }

}