/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{

    public class Shipment : IShipment
    {
        virtual public string ShipperRatePlan { get; set; }
        virtual public string IntegratorCarrierId { get; set; }
        virtual public string IntegratorRatePlan { get; set; }
        virtual public string IntegratorId { get; set; }
        virtual public string MinimalAddressValidation { get; set; }
        virtual public string TransactionId { get; set; }
        virtual public IAddress FromAddress { get; set; }
        virtual public IAddress ToAddress { get; set; }
        virtual public IAddress AltReturnAddress { get; set; }
        virtual public IParcel Parcel { get; set; }
        virtual public IEnumerable<IRates> Rates { get; set; }
        virtual public IRates AddRates(IRates r)
        {
            return ModelHelper.AddToEnumerable<IRates, Rates>(r, () => Rates, (x) => Rates = x);
        }
        virtual public IEnumerable<IDocument> Documents { get; set; }
        virtual public IDocument AddDocument(IDocument d)
        {
            return ModelHelper.AddToEnumerable<IDocument, Document>(d, () => Documents, (x) => Documents = x);
        }
        virtual public IEnumerable<IShipmentOptions> ShipmentOptions { get; set; }
        virtual public IShipmentOptions AddShipmentOptions(IShipmentOptions s)
        {
            return ModelHelper.AddToEnumerable<IShipmentOptions, ShipmentOptions>(s, () => ShipmentOptions, (x) => ShipmentOptions = x);
        }
        virtual public ShipmentType ShipmentType { get; set; }
        virtual public ICustoms Customs { get; set; }
        virtual public string ShipmentId { get; set; }
        virtual public string ParcelTrackingNumber { get; set; }
    }
}