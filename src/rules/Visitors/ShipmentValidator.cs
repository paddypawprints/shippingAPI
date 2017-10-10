namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public class ShipmentValidator : IRateRuleVisitor
    {
        private IShipment _shipment;
        private IRates _rate { get; set; }

        public bool IsValid { get; private set; }


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
            if (_rate.Carrier != carrierRule.Carrier) return;
            if (Shipment.ToAddress.CountryCode != carrierRule.DestinationCountry) return;
            if (Shipment.FromAddress.CountryCode != carrierRule.OriginCountry) return;
            if (!carrierRule.ServiceRules.ContainsKey(_rate.ServiceId)) return;
            foreach (var rule in carrierRule.ServiceRules[_rate.ServiceId])
            {
                rule.Accept(this);
            }
        }

        public void Visit(ServiceRule serviceRule)
        {
            if (_rate.ServiceId != serviceRule.ServiceId) return;
            if (!serviceRule.ParcelTypeRules.ContainsKey(_rate.ParcelType)) return;
            foreach ( var rule in serviceRule.ParcelTypeRules[_rate.ParcelType])
            {
                if (rule is ParcelTypeRule r) r.Accept(this);
            }
        }

        public void Visit(ParcelTypeRule parcelRule)
        {
            if (_rate.ParcelType != parcelRule.ParcelType) return;
            foreach (var ss in _rate.SpecialServices)
            {
                if (!parcelRule.SpecialServiceRules.ContainsKey(ss.SpecialServiceId)) return;
            }
            foreach (var ss in _rate.SpecialServices)
            {
                if (!parcelRule.SpecialServiceRules.ContainsKey(ss.SpecialServiceId)) return;
                foreach (var rule in parcelRule.SpecialServiceRules[ss.SpecialServiceId])
                {
                    if (rule is SpecialServicesRule r) r.Accept(this);
                }
            }
 
        }

        public void Visit(SpecialServicesRule specialServicesRule)
        {
            foreach (var ss in _rate.SpecialServices)
            {
                if ( ss.SpecialServiceId == specialServicesRule.SpecialServiceId)
                {
                    if(specialServicesRule.InputParameterRules != null && specialServicesRule.InputParameterRules.Count > 1)
                    if (!specialServicesRule.IsValidParameters(ss)) return;
                }
                else
                {
                    if (specialServicesRule.PrerequisiteRules != null && specialServicesRule.IncompatibleSpecialServices.Contains(ss.SpecialServiceId))
                    {
                        return;
                    }
                }
            }
            IsValid = true;
        }
    }
}
