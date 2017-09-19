using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    /// <summary>
    /// Street address and/or apartment and/or P.O. Box. You can specify up to three address lines.
    /// </summary>
    public class  Address : IAddress
    {
        virtual public IEnumerable<string> AddressLines { get; set; }
        virtual public void AddAddressLine(string s)
        {
            if (AddressLines as List<string> != null )
            {
                if ((AddressLines as List<string>).Count == 3) throw new InvalidOperationException("Address can only have 3 lines");
            }
            ModelHelper.AddToEnumerable<string, string>(s,()=>AddressLines, (x)=>AddressLines = x);
        }
        virtual public string CityTown { get; set;}
        virtual public string StateProvince { get;set;}
        virtual public string PostalCode {get;set;}
        virtual public string CountryCode {get;set;}
        virtual public string Company {get;set;}
        virtual public string Name {get;set;}
        virtual public string Phone { get;set;}
        virtual public string Email {get;set;}
        virtual public bool Residential {get;set;}
        virtual public AddressStatus Status { get;set;}
    }
}