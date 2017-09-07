﻿using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IShipment
    {
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
        string ShipmentId { get; set; }
        string ParcelTrackingNumber { get; set; }
    }
}