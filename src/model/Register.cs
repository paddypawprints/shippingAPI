using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public static class Model
    {
        public static void RegisterSerializationTypes( SerializationRegistry registry)
        {
            registry.RegisterSerializationTypes<IAddress, Address>();
            registry.RegisterSerializationTypes<IAutoRefill, AutoRefill>();
            registry.RegisterSerializationTypes<ICarrierPickup, CarrierPickup>();
            registry.RegisterSerializationTypes<ICcPaymentDetails, CcPaymentDetails>();
            registry.RegisterSerializationTypes<ICountry, Country>();
            registry.RegisterSerializationTypes<ICustoms, Customs>();
            registry.RegisterSerializationTypes<ICustomsItems, CustomsItems>();
            registry.RegisterSerializationTypes<ICustomsInfo, CustomsInfo>();
            registry.RegisterSerializationTypes<IDeliveryCommitment, DeliveryCommitment>();
            registry.RegisterSerializationTypes<IDocument, Document>();
            registry.RegisterSerializationTypes<IManifest, Manifest>();
            registry.RegisterSerializationTypes<IMerchant, Merchant>();
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
