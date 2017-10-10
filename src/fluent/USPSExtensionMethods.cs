using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Fluent;

namespace PitneyBowes.Developer.ShippingApi
{
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
    public enum PMODPaymentMethod
    {
        ELECTRONIC,
        NONELECTRONIC
    }
    public static class USPSExtensions
    {
        public static RatesArrayFluent<T> USPSPriority<T,P>(this RatesArrayFluent<T> f) 
            where T : class, IRates, new()
            where P : class, IParameter, new()
        {
            return f.Add().Carrier(Carrier.USPS)
                .ParcelType(ParcelType.PKG)
                .Service(Services.PM)
                .SpecialService<SpecialServices>(SpecialServiceCodes.DelCon, 0M, new P { Name = "INPUT_VALUE", Value = "0"  });
        }

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
