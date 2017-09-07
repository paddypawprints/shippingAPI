using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public class Country
    {
        public static Dictionary<string, string> Rules = new Dictionary<string, string>();
        public static void Add(IEnumerable<ICountry> countryList)
        {
            foreach( var c in countryList )
            {
                Rules[c.CountryCode] = c.CountryName;
            }
        }
        public static bool Validate(IAddress address)
        {
            return Rules.ContainsKey(address.CountryCode);
        }
    }

    public class Service
    {

    }
    public class PackageTypeRules
    {

    }

    public class SpecialservicesRules
    {

    }
    public class CarrierRules
    {
        public static void Add(ICarrierRule carrierRule)
        {

        }
    }
}
