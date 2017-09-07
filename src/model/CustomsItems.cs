
using System;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CustomsItems : ICustomsItems
    {

        virtual public string Description { get; set;}

        virtual public int Quantity { get; set;}

        virtual public decimal UnitPrice { get;set;}

        virtual public IParcelWeight UnitWeight {get;set;}

        virtual public string HSTariffCode {get;set;}

        virtual public string OriginCountryCode {get;set;}

    }

}