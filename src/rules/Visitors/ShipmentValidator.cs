/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public class ShipmentValidator : IRateRuleVisitor
    {
        public enum ValidationState
        {
            INVALID,
            VALID,
            PROCESSING
        }
        private ValidationState _state;
        private IShipment _shipment;
        private IRates _rate { get; set; }

        public ShipmentValidator()
        {
            _state = ValidationState.PROCESSING;
        }
        public bool IsValid
        {
            get
            {
                return _state == ValidationState.VALID;
            }
        }
        public string Reason { get; set; }

        public bool Validate(IShipment shipment, CarrierRule rules)
        {
            Shipment = shipment;
            Visit(rules);
            return IsValid;
        }

        public IShipment Shipment
        {
            get
            {
                return _shipment;
            }
            set
            {
                _shipment = value;
                var i = _shipment.Rates.GetEnumerator();
                i.MoveNext();
                _rate = i.Current;
            }
        }

        public void Visit(CarrierRule carrierRule)
        {
            if (_rate.Carrier != carrierRule.Carrier)
            {
                _state = ValidationState.INVALID;
                Reason = string.Format("Carrier {0} is not contained in rules", _rate.Carrier);
                return;
            }
            if (Shipment.ToAddress.CountryCode != carrierRule.DestinationCountry)
            {
                _state = ValidationState.INVALID;
                Reason = string.Format("Destination country {0} is not contained in rules", Shipment.ToAddress.CountryCode);
                return;
            }
            if (Shipment.FromAddress.CountryCode != carrierRule.OriginCountry)
            {
                _state = ValidationState.INVALID;
                Reason = string.Format("Origin country {0} is not contained in rules", Shipment.FromAddress.CountryCode);
                return;
            }
            if (!carrierRule.ServiceRules.ContainsKey(_rate.ServiceId))
            {
                _state = ValidationState.INVALID;
                Reason = string.Format("Carrier {0} does not support format {1}", _rate.Carrier, _rate.ServiceId);
                return;
            }
            foreach (var rule in carrierRule.ServiceRules[_rate.ServiceId])
            {
                if ( _state == ValidationState.PROCESSING)
                    rule.Accept(this);
            }
            if (_state == ValidationState.PROCESSING)
            {
                _state = ValidationState.VALID;
                Reason = "Valid";
            }
        }

        public void Visit(ServiceRule serviceRule)
        {
            if (_rate.ServiceId != serviceRule.ServiceId) return;

            if (!serviceRule.ParcelTypeRules.ContainsKey(_rate.ParcelType))
            {
                _state = ValidationState.INVALID;
                Reason = string.Format("Service {0} does not support parcel type {1}", serviceRule.ServiceId, _rate.ParcelType);
                return;
            }
            foreach ( var rule in serviceRule.ParcelTypeRules[_rate.ParcelType])
            {
                if (_state == ValidationState.PROCESSING)
                    rule.Accept(this);
            }
        }

        public void Visit(ParcelTypeRule parcelRule)
        {
            if (_rate.ParcelType != parcelRule.ParcelType) return;

            foreach (var ss in _rate.SpecialServices)
            {
                if (!parcelRule.SpecialServiceRules.ContainsKey(ss.SpecialServiceId))
                {
                    Reason = string.Format("Parcel type {0} does not support special service type {1}", parcelRule.ParcelType, ss.SpecialServiceId);
                    _state = ValidationState.INVALID;
                    return;
                }
                if (!parcelRule.FitsDimensions(_shipment.Parcel.Dimension))
                { 
                    Reason = string.Format("Parcel is outside of the dimension requirements for {0}", parcelRule.ParcelType);
                    _state = ValidationState.INVALID;
                    return;
                }
                if (!parcelRule.HoldsWeight(_shipment.Parcel.Weight))
                {
                    Reason = string.Format("Parcel is outside of the weight requirements for {0}", parcelRule.ParcelType);
                    _state = ValidationState.INVALID;
                    return;
                }
                foreach (var rule in parcelRule.SpecialServiceRules[ss.SpecialServiceId])
                {
                    if (_state == ValidationState.PROCESSING)
                        rule.Accept(this);
                }
            }
 
        }

        public void Visit(SpecialServicesRule specialServicesRule)
        {
            foreach (var ss in _rate.SpecialServices)
            {
                if ( ss.SpecialServiceId == specialServicesRule.SpecialServiceId)
                {
                    if (specialServicesRule.InputParameterRules != null && specialServicesRule.InputParameterRules.Count > 1)
                    {
                        if (!specialServicesRule.HasRequiredParameters(ss))
                        {
                            _state = ValidationState.INVALID;
                            Reason = string.Format("Special service {0} is missing required parameters", ss.SpecialServiceId);
                            return;
                        }
                        if (!specialServicesRule.IsValidParameterValues(ss))
                        {
                            Reason = string.Format("Special service {0} has value outside the permissable range", ss.SpecialServiceId);
                            _state = ValidationState.INVALID;
                            return;
                        }
                    }
                }
                else
                {
                    if (specialServicesRule.IncompatibleSpecialServices != null && specialServicesRule.IncompatibleSpecialServices.Contains(ss.SpecialServiceId))
                    {
                        Reason = string.Format("Special service {0} is incompatible with other selected services", ss.SpecialServiceId);
                        _state = ValidationState.INVALID;
                        return;
                    }
                }
            }
            if (specialServicesRule.PrerequisiteRules != null && !specialServicesRule.IsValidPrerequisites(_rate.SpecialServices))
            {
                Reason = string.Format("Special service {0} is missing prerequisites", specialServicesRule.SpecialServiceId);
                _state = ValidationState.INVALID;
                return;

            }

        }
    }
}
