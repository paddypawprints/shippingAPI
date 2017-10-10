/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;
using System.Collections;
using System.Collections.Generic;

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
