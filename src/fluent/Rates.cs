using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace com.pb.shippingapi.fluent
{
    public class Rates
    {
        private model.Rates _rates;

        public static Rates Create()
        {
            return new Rates();
        }
        public Rates()
        {
            _rates = new model.Rates();
        }

        public static implicit operator model.Rates( Rates r )
        {
            return r._rates;
        }
        public Rates Carrier(model.Carrier c) 
        { 
            _rates.Carrier = c;
            return this;
        }

        public Rates Service(model.USPSServices s) 
        {
            _rates.serviceId = s;
            return this;
        }
        public Rates ParcelType(model.USPSParcelType t) 
        { 
            _rates.ParcelType = t;
            return this;
        }

        public Rates specialService( model.USPSSpecialServiceCodes c, decimal f, params object[] p)
        {
            if (_rates.specialServices == null ) _rates.specialServices = new List<model.SpecialServices>();
            var l = (List<model.SpecialServices> ) _rates.specialServices;
            var s = new model.SpecialServices() {SpecialServiceId = c, Fee = f};
            if ( s.InputParameters == null ) s.InputParameters = new List<model.Parameter>();
            foreach( dynamic i in p )
            {
                ((List<model.Parameter>)s.InputParameters).Add( new model.Parameter() {Name = i.Name, Value = i.Value} );
            }
            l.Add( s ); 
            return this;
        }

        public Rates InductionPostalCode(string s) 
        { 
            _rates.InductionPostalCode = s;
            return this;
        }

        public Rates DimensionalWeight(decimal w, model.UnitOfWeight u) 
        { 
            _rates.DimensionalWeight = new model.ParcelWeight(){Weight = w, UnitOfMeasurement = u};
            return this;
        }
         public Rates DeliveryCommitment(model.DeliveryCommitment c) 
         { 
             _rates.DeliveryCommitment = c;
             return this;
         }

        public Rates CurrencyCode(string s) 
        { 
            _rates.CurrencyCode = s;
            return this;
        }


    }
}