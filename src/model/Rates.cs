using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Rates : IRates
    {
        virtual public Carrier Carrier { get; set;}
        virtual public Services ServiceId { get;set;}
        virtual public ParcelType ParcelType { get; set;}
        virtual public IEnumerable<ISpecialServices> SpecialServices { get; set; }
        virtual public ISpecialServices AddSpecialservices( ISpecialServices s)
        {
            return ModelHelper.AddToEnumerable<ISpecialServices, SpecialServices>(s, () => SpecialServices, (x) => SpecialServices = x);
        }
        virtual public string InductionPostalCode { get; set;}
        virtual public IParcelWeight DimensionalWeight { get; set; }
        virtual public decimal BaseCharge { get; set;}
        virtual public decimal TotalCarrierCharge { get; set;}
        virtual public decimal AlternateBaseCharge { get; set;}
        virtual public decimal AlternateTotalCharge { get; set;}
        virtual public IDeliveryCommitment DeliveryCommitment { get; set; }
        virtual public string CurrencyCode { get; set;}
        virtual public int DestinationZone { get; set;}
    }
}