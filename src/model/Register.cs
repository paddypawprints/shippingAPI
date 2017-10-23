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

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public static class Model
    {
        public static void RegisterSerializationTypes( SerializationRegistry registry)
        {
            registry.RegisterSerializationTypes<IAddress, Address>();
            registry.RegisterSerializationTypes<IAutoRefill, AutoRefill>();
            registry.RegisterSerializationTypes<ICcPaymentDetails, CcPaymentDetails>();
            registry.RegisterSerializationTypes<ICountry, Country>();
            registry.RegisterSerializationTypes<ICustoms, Customs>();
            registry.RegisterSerializationTypes<ICustomsItems, CustomsItems>();
            registry.RegisterSerializationTypes<ICustomsInfo, CustomsInfo>();
            registry.RegisterSerializationTypes<IDeliveryCommitment, DeliveryCommitment>();
            registry.RegisterSerializationTypes<IDocument, Document>();
            registry.RegisterSerializationTypes<IManifest, Manifest>();
            registry.RegisterSerializationTypes<IMerchant, Merchant>();
            registry.RegisterSerializationTypes<IPage, Page>();
            registry.RegisterSerializationTypes<IParameter, Parameter>();
            registry.RegisterSerializationTypes<IParcel, Parcel>();
            registry.RegisterSerializationTypes<IParcelDimension, ParcelDimension>();
            registry.RegisterSerializationTypes<IParcelWeight, ParcelWeight>();
            registry.RegisterSerializationTypes<IPaymentInfo, PaymentInfo>();
            registry.RegisterSerializationTypes<IPickup, Pickup>();
            registry.RegisterSerializationTypes<IPickupCount, PickupCount>();
            registry.RegisterSerializationTypes<IPpPaymentDetails, PpPaymentDetails>();
            registry.RegisterSerializationTypes<IRates, Rates>();
            registry.RegisterSerializationTypes<IShipment, Shipment>();
            registry.RegisterSerializationTypes<IShipmentOptions, ShipmentOptions>();
            registry.RegisterSerializationTypes<ISpecialServices, SpecialServices>();
            registry.RegisterSerializationTypes<IToken, Token>();
            registry.RegisterSerializationTypes<ITrackingEvent, TrackingEvent>();
            registry.RegisterSerializationTypes<ITrackingStatus, TrackingStatus>();
            registry.RegisterSerializationTypes<ITransaction, Transaction>();
            registry.RegisterSerializationTypes<ITransactionSort, TransactionSort>();
            registry.RegisterSerializationTypes<IUserInfo, UserInfo>();
        }
    }
}
