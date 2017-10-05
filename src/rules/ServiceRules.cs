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

        private static void Load(ISession session = null)
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
        public static bool Validate(string countryCode, ISession session)
        {
            Load(session);
            lock (_lock)
            {
                return Rules.ContainsKey(countryCode);
            }
        }
    }

    public static class DictionaryConvenienceExtensions
    {
        public static void CreateAndAdd<K,V>( this Dictionary<K,List<V>> dictionary, K key, V value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new List<V>());
            }
            dictionary[key].Add(value);
        }
    }

    public class CarrierRulesLookupResult
    {
        Carrier Carrier { get; set; }
        Services Service { get; set; }
        ParcelType ParcelType { get; set; }
        SpecialServiceCodes SpecialServiceCodes { get; set; }
    }

    public class CarrierRulesCache
    {
        public Dictionary<Carrier, ICarrierRule> CarrierLookup { get; set; }
        public Dictionary<Services, ICarrierRule> ServiceLookup { get; set; }
        public Dictionary<ParcelType, List<ICarrierRule>> ParcelCarrierLookup { get; set; }
        public Dictionary<SpecialServiceCodes, List<ICarrierRule>> SpecialServiceCarrierLookup { get; set; }
        public Dictionary<Services, List<IParcelTypeRule>> ServiceParcelTypeLookup { get; set; }
        public Dictionary<ParcelType, IParcelTypeRule> ParcelLookup { get; set; }
        public Dictionary<SpecialServiceCodes, List<IParcelTypeRule>> SpecialServiceParcelLookup { get; set; }
        public Dictionary<Services, List<ISpecialServicesRule>> ServiceSpecialServicesLookup { get; set; }
        public Dictionary<ParcelType, List<ISpecialServicesRule>> ParcelSpecialServicesLookup { get; set; }
        public Dictionary<SpecialServiceCodes, ISpecialServicesRule> SpecialServicesLookup { get; set; }

        public bool IsEqual(ICarrierRule r1, ICarrierRule r2)
        {
            return r1.BrandedName == r2.BrandedName 
                && r1.ServiceId == r2.ServiceId;
        }

        public bool IsEqual(IParcelTypeRule r1, IParcelTypeRule r2)
        {
            return r1.BrandedName.Equals(r2.BrandedName)
                && r1.ParcelType == r2.ParcelType
                && r1.RateTypeBrandedName.Equals(r2.RateTypeBrandedName)
                && r1.RateTypeId.Equals(r2.RateTypeId);
        }

        public bool IsEqual(ISpecialServicesRule r1, ISpecialServicesRule r2)
        {
            return r1.BrandedName.Equals(r2.BrandedName)
                && r1.CategoryId.Equals(r2.CategoryId)
                && r1.CategoryName.Equals(r2.CategoryName)
                && r1.SpecialServiceId == r2.SpecialServiceId;
        }


        public void Add(Carrier carrier, ICarrierRule carrierRule)
        {
            CarrierLookup.Add(carrierRule.Carrier, carrierRule);
            //ServiceLookup.Add(carrierRule.ServiceId, carrierRule);
            
        //public Dictionary<SpecialServiceCodes, List<ICarrierRule>> SpecialServiceCarrierLookup { get; set; }
        //public Dictionary<Services, List<IParcelTypeRule>> ServiceParcelTypeLookup { get; set; }
        //public Dictionary<ParcelType, IParcelTypeRule> ParcelLookup { get; set; }
        //public Dictionary<SpecialServiceCodes, List<IParcelTypeRule>> SpecialServiceParcelLookup { get; set; }
        //public Dictionary<Services, List<ISpecialServicesRule>> ServiceSpecialServicesLookup { get; set; }
        //public Dictionary<ParcelType, List<ISpecialServicesRule>> ParcelSpecialServicesLookup { get; set; }
        //public Dictionary<SpecialServiceCodes, ISpecialServicesRule> SpecialServicesLookup { get; set; }



            foreach (var p in carrierRule.ParcelTypeRules)
            {
                ParcelCarrierLookup.CreateAndAdd(p.ParcelType, carrierRule);
                foreach (var s in p.SpecialServiceRules)
                {
                    //SpecialServiceCarrierLookup.CreateAndAdd<SpecialServiceCodes, ICarrierRule>(s.SpecialServiceId, carrierRule);
                    //SpecialServicesRulesCache.Add(s);
                    CarrierRulesCache.Associate(carrierRule, p, s);
                    //ParcelTypeRulesCache.Associate(carrierRule, p, s);
                    //SpecialServicesRulesCache.Associate(carrierRule, p, s);
                }
            }

        }

        public static void Associate(ICarrierRule carrierRule, IParcelTypeRule parcelTypeRule, ISpecialServicesRule specialServicesRule)
        {

        }
        public static IEnumerable<ICarrierRule> Lookup( IEnumerable<IParcelTypeRule> parcels, IEnumerable<ISpecialServicesRule> services)
        {
            throw new NotImplementedException();
        }
    }
}
