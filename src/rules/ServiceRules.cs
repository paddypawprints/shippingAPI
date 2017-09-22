using System;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Method;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public class CountryRule
    {
        public class Country : ICountry
        {
            public string CountryCode { get; set; }
            public string CountryName { get; set; }
        }

        private static object _lock = new object();
        public static Dictionary<string, string> Rules = new Dictionary<string, string>();
        public static DateTimeOffset? LastUpdate { get; set; }

        private static void Load(Session session = null)
        {
            if (LastUpdate == null || DateTimeOffset.Now - LastUpdate > TimeSpan.FromHours(1))
            {
                lock (_lock)
                {
                    // Load countries
                    var countriesRequest = new CountriesRequest<Country>()
                    {
                        Carrier = Carrier.USPS,
                        OriginCountryCode = "US"
                    };
                    var countriesResponse = CountriesMethods.Countries(countriesRequest, session).GetAwaiter().GetResult();
                    if (countriesResponse.Success)
                    {
                        Rules.Clear();
                        foreach (var c in countriesResponse.APIResponse)
                        {
                            Rules[c.CountryCode] = c.CountryName;
                        }
                        LastUpdate = DateTimeOffset.Now;
                    }
                    else
                    {
                        //TODO: Unhappy
                    }
                }
            }
        }
        public static bool Validate(string countryCode, Session session)
        {
            Load(session);
            lock (_lock)
            {
                return Rules.ContainsKey(countryCode);
            }
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
        public static Dictionary<string, ISpecialServicesRule> SpecialServicesRule = new Dictionary<string, ISpecialServicesRule>();
    }
    public class CarrierRules
    {
        public static void Add(ICarrierRule carrierRule)
        {

        }
    }
}
