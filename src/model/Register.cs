using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public static class Model
    {
        public static void RegisterSerializationTypes( Session session)
        {
            session.RegisterSerializationTypes<IAddress, Address>();
            session.RegisterSerializationTypes<IAutoRefill, AutoRefill>();
            session.RegisterSerializationTypes<ICarrierPickup, CarrierPickup>();
            session.RegisterSerializationTypes<ICarrierRule, CarrierRule>();
            session.RegisterSerializationTypes<ICcPaymentDetails, CcPaymentDetails>();
            session.RegisterSerializationTypes<ICountry, Country>();
            session.RegisterSerializationTypes<ICustoms, Customs>();
            session.RegisterSerializationTypes<ICustomsItems, CustomsItems>();
            session.RegisterSerializationTypes<ICustomsInfo, CustomsInfo>();
            session.RegisterSerializationTypes<IDeliveryCommitment, DeliveryCommitment>();
            session.RegisterSerializationTypes<IDocument, Document>();
            session.RegisterSerializationTypes<IDimensionRule, DimensionRule>();
            session.RegisterSerializationTypes<IManifest, Manifest>();
            session.RegisterSerializationTypes<IMerchant, Merchant>();
            session.RegisterSerializationTypes<IParameter, Parameter>();
            session.RegisterSerializationTypes<IParcel, Parcel>();
            session.RegisterSerializationTypes<IParcelDimension, ParcelDimension>();
            session.RegisterSerializationTypes<IParcelTypeRule, ParcelTypeRule>();
            session.RegisterSerializationTypes<IParcelWeight, ParcelWeight>();
            session.RegisterSerializationTypes<IPaymentInfo, PaymentInfo>();
            session.RegisterSerializationTypes<IPickup, Pickup>();
            session.RegisterSerializationTypes<IPickupCount, PickupCount>();
            session.RegisterSerializationTypes<IPpPaymentDetails, PpPaymentDetails>();
            session.RegisterSerializationTypes<IRates, Rates>();
            session.RegisterSerializationTypes<IServicesParameterRule, ServicesParameterRule>();
            session.RegisterSerializationTypes<IServicesPrerequisiteRule, ServicesPrerequisiteRule>();
            session.RegisterSerializationTypes<IShipment, Shipment>();
            session.RegisterSerializationTypes<IShipmentOptions, ShipmentOptions>();
            session.RegisterSerializationTypes<ISpecialServices, SpecialServices>();
            session.RegisterSerializationTypes<ISpecialServicesRule, SpecialServicesRule>();
            session.RegisterSerializationTypes<IToken, Token>();
            session.RegisterSerializationTypes<ITrackingEvent, TrackingEvent>();
            session.RegisterSerializationTypes<ITrackingStatus, TrackingStatus>();
            session.RegisterSerializationTypes<ITransaction, Transaction>();
            session.RegisterSerializationTypes<ITransactionSort, TransactionSort>();
            session.RegisterSerializationTypes<IUserInfo, UserInfo>();
            session.RegisterSerializationTypes<IWeightRule, WeightRule>();
        }
    }
}
