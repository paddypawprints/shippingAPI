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

using PitneyBowes.Developer.ShippingApi.Model;

namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    /// <summary>
    /// USPS facility types for workshare/injection - DDU, SCF etc.
    /// </summary>
    public enum USPSFacility
    {
        /// <summary>
        /// Destination Delivery Unit
        /// </summary>
        DDU,
        /// <summary>
        /// Network Distribution Center
        /// </summary>
        NDC,
        /// <summary>
        /// Auxiliary Service Center
        /// </summary>
        ASF,
        /// <summary>
        /// Sectional Center Facility
        /// </summary>
        SCF,
        /// <summary>
        /// Area Distribution Center
        /// </summary>
        ADC
    }

    /// <summary>
    /// PMOD payment method.
    /// </summary>
    public enum PMODPaymentMethod
    {
        ELECTRONIC,
        NONELECTRONIC
    }

    /// <summary>
    /// USPS Extensions. Extension methods to support USPS products and servives in the fluent interface.
    /// </summary>
    public static class USPSExtensions
    {
        /// <summary>
        /// Set up the rates object for USPS priority mail service.
        /// </summary>
        /// <returns>this</returns>
        /// <param name="f">this</param>
        /// <typeparam name="T">Type of the IRates concrete class.</typeparam>
        /// <typeparam name="P">Type of the IParameter concreate class.</typeparam>
        public static RatesArrayFluent<T> USPSPriority<T,P>(this RatesArrayFluent<T> f) 
            where T : class, IRates, new()
            where P : class, IParameter, new()
        {
            return f.Add().Carrier(Carrier.USPS)
                .ParcelType(ParcelType.PKG)
                .Service(Services.PM)
                .SpecialService<SpecialServices>(SpecialServiceCodes.DelCon, 0M, new P { Name = "INPUT_VALUE", Value = "0"  });
        }
        /// <summary>
        /// Returns the shipment.
        /// </summary>
        /// <returns>The shipment.</returns>
        /// <param name="f">this</param>
        /// <typeparam name="T">Type of the IShipment</typeparam>
        public static ShipmentFluent<T> ReturnShipment<T>(this ShipmentFluent<T> f) where T : class, IShipment, new()
        {
            T shipment = ((T)f);
            if (shipment.ShipmentOptions != null)
            {
                foreach (var o in shipment.ShipmentOptions)
                {
                    if (o.ShipmentOption == ShipmentOption.ADD_TO_MANIFEST) o.Value = "false";
                }
            }
            return f.ShipmentType(ShipmentType.RETURN).ShipperRatePlan(null);
        }
        /// <summary>
        /// Set shipment options for PMOD.
        /// </summary>
        /// <returns>this</returns>
        /// <param name="f">this</param>
        /// <param name="originEntryFacility">Origin entry facility.</param>
        /// <param name="destinationEntryFacility">Destination entry facility.</param>
        /// <param name="enclosedMailClass">Enclosed mail class.</param>
        /// <param name="enclosedParcelType">Enclosed parcel type.</param>
        /// <param name="paymentMethod">Payment method.</param>
        /// <typeparam name="T">Type of the shipment options concrete class.</typeparam>
        public static ShipmentOptionsArrayFluent<T> PMODOptions<T>(this ShipmentOptionsArrayFluent<T> f, USPSFacility originEntryFacility, USPSFacility destinationEntryFacility, Services enclosedMailClass, ParcelType enclosedParcelType, PMODPaymentMethod  paymentMethod ) where T : class, IShipmentOptions, new()
        {
            return f
                .AddOption(ShipmentOption.ORIGIN_ENTRY_FACILITY, originEntryFacility.ToString())
                .AddOption(ShipmentOption.DESTINATION_ENTRY_FACILITY, destinationEntryFacility.ToString())
                .AddOption(ShipmentOption.ENCLOSED_MAIL_CLASS, enclosedMailClass.ToString())
                .AddOption(ShipmentOption.ENCLOSED_PARCEL_TYPE, enclosedParcelType.ToString())
                .AddOption(ShipmentOption.ENCLOSED_PAYMENT_METHOD, paymentMethod.ToString());
        }
    }
}
