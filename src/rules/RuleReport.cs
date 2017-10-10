using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public class RuleReport : IEnumerable<Tuple<CarrierRule, ServiceRule, ParcelTypeRule, SpecialServicesRule>>
    {
        public IEnumerable<CarrierRule> CarrierRules { get; set; }
        public Func<CarrierRule, bool> CarrierRuleFilter { get; set; }
        public Func<ServiceRule, bool> ServiceRuleFilter { get; set; }
        public Func<ParcelTypeRule, bool> ParcelTypeRuleFilter { get; set; }
        public Func<SpecialServicesRule, bool> SpecialServicesRuleFilter { get; set; }

        public IEnumerator<Tuple<CarrierRule, ServiceRule, ParcelTypeRule, SpecialServicesRule>> GetEnumerator()
        {
            if (CarrierRules != null)
            {
                foreach (var carrierRule in CarrierRules)
                {
                    if (CarrierRuleFilter == null || CarrierRuleFilter(carrierRule))
                    {
                        foreach (var serviceRule in carrierRule.ServiceRules)
                        {
                            if (ServiceRuleFilter == null || ServiceRuleFilter(serviceRule))
                            {
                                foreach (var parcelTypeRule in serviceRule.ParcelTypeRules)
                                {
                                    if (ParcelTypeRuleFilter == null || ParcelTypeRuleFilter(parcelTypeRule))
                                    {
                                        if (parcelTypeRule.SpecialServiceRules == null)
                                        {
                                            yield return new Tuple<CarrierRule, ServiceRule, ParcelTypeRule, SpecialServicesRule>
                                                (carrierRule, serviceRule, parcelTypeRule, null);
                                        }
                                        else
                                        {
                                            foreach (var specialServicesRule in parcelTypeRule.SpecialServiceRules)
                                            {
                                                if (SpecialServicesRuleFilter == null || SpecialServicesRuleFilter(specialServicesRule))
                                                {
                                                    yield return new Tuple<CarrierRule, ServiceRule, ParcelTypeRule, SpecialServicesRule>
                                                        (carrierRule, serviceRule, parcelTypeRule, specialServicesRule);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
