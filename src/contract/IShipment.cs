/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IShipment
    {
        string TransactionId { get; set; }
        string MinimalAddressValidation { get; set; }
        string ShipperRatePlan { get; set; }
        IAddress FromAddress { get; set; }
        IAddress ToAddress { get; set; }
        IAddress AltReturnAddress { get; set; }
        IParcel Parcel { get; set; }
        IEnumerable<IRates> Rates { get; set; }
        IRates AddRates(IRates r);
        IEnumerable<IDocument> Documents { get; set; }
        IDocument AddDocument(IDocument d);
        IEnumerable<IShipmentOptions> ShipmentOptions { get; set; }
        IShipmentOptions AddShipmentOptions(IShipmentOptions o);
        ICustoms Customs { get; set; }
        ShipmentType ShipmentType { get; set; }
        string ShipmentId { get; set; }
        string ParcelTrackingNumber { get; set; }
    }
}