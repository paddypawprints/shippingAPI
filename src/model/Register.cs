using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public static class Model
    {
        public static void RegisterSerializationTypes( ShippingApi.Session session)
        {
            session.RegisterSerializationTypes<IShipment, Shipment>();
            session.RegisterSerializationTypes<IRates, Rates>();
            session.RegisterSerializationTypes<IAddress, Address>();
            session.RegisterSerializationTypes<IParameter, Parameter>();
            session.RegisterSerializationTypes<IParcel, Parcel>();
            session.RegisterSerializationTypes<ICustoms, Customs>();
            session.RegisterSerializationTypes<ICustomsItems, CustomsItems>();
            session.RegisterSerializationTypes<ICustomsInfo, CustomsInfo>();
            session.RegisterSerializationTypes<IParcelDimension, ParcelDimension>();
            session.RegisterSerializationTypes<IParcelWeight, ParcelWeight>();
            session.RegisterSerializationTypes<IDocument, Document>();
            session.RegisterSerializationTypes<IShipmentOptions, ShipmentOptions>();
            session.RegisterSerializationTypes<ISpecialServices, SpecialServices>();
            session.RegisterSerializationTypes<IToken, Token>();
            session.RegisterSerializationTypes<ITransaction, Transaction>();
        }
    }
}
