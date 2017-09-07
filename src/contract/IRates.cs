using Newtonsoft.Json;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IRates
    {
        Carrier Carrier { get; set; }
        USPSServices ServiceId { get; set; }
        USPSParcelType ParcelType { get; set; }
        IEnumerable<ISpecialServices> SpecialServices { get; set; }
        ISpecialServices AddSpecialservices(ISpecialServices s);
        string InductionPostalCode { get; set; }
        IParcelWeight DimensionalWeight { get; set; }
        decimal BaseCharge { get; set; }
        decimal TotalCarrierCharge { get; set; }
        decimal AlternateBaseCharge { get; set; }
        decimal AlternateTotalCharge { get; set; }
        IDeliveryCommitment DeliveryCommitment { get; set; }
        string CurrencyCode { get; set; }
        int DestinationZone { get; set; }
    }
}