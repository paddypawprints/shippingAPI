using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{

    public class Shipment : IShipment
    {
        virtual public string CorrelationId { get; set; }
        virtual public string IntegratorCarrierId { get; set; }
        virtual public string IntegratorRatePlan { get; set; }
        virtual public string IntegratorId { get; set; }
        virtual public string IsCharged { get; set; }
        virtual public string MessageWeight { get; set; }
        virtual public string PartnerId { get; set; }
        virtual public string TransactionId { get; set; }
        virtual public string RequestId { get; set; }
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
        virtual public ICustoms Customs { get; set; }
        virtual public string ShipmentId { get; set; }
        virtual public string ParcelTrackingNumber { get; set; }

    }
}