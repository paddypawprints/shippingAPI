using Newtonsoft.Json;
using System.Collections.Generic;
using com.pb.shippingapi.model;

namespace com.pb.shippingapi.fluent
{
    public class RatesArrayFluent : List<Rates>
    {
        public static RatesArrayFluent Create()
        {
            return new RatesArrayFluent();
        }
        protected Rates _current = null;

        public RatesArrayFluent Add()
        {
            Add(new Rates());
            _current = FindLast((x) => true);
            return this;
        }

        public RatesArrayFluent First()
        {
            _current = Find((x) => true);
            return this;
        }

        public RatesArrayFluent Next()
        {
            var i = IndexOf(_current);
            _current = this[i + 1];
            return this;
        }

        public bool IsLast()
        {
            var i = IndexOf(_current);
            return (i == Count - 1);
        }

        public RatesArrayFluent Carrier(Carrier c) 
        {
            _current.Carrier = c;
            return this;
        }

        public RatesArrayFluent Service(USPSServices s) 
        {
            _current.serviceId = s;
            return this;
        }
        public RatesArrayFluent ParcelType(USPSParcelType t) 
        {
            _current.ParcelType = t;
            return this;
        }

        public RatesArrayFluent specialService(USPSSpecialServiceCodes c, decimal f, params Parameter[] p)
        {
            if (_current.specialServices == null ) _current.specialServices = new List<SpecialServices>();
            var l = (List<SpecialServices> )_current.specialServices;
            var s = new SpecialServices() {SpecialServiceId = c, Fee = f};
            if ( s.InputParameters == null ) s.InputParameters = new List<Parameter>();
            foreach( var i in p )
            {
                ((List<Parameter>)s.InputParameters).Add( i );
            }
            l.Add( s ); 
            return this;
        }

        public RatesArrayFluent InductionPostalCode(string s) 
        {
            _current.InductionPostalCode = s;
            return this;
        }

        public RatesArrayFluent DimensionalWeight(decimal w, UnitOfWeight u) 
        {
            _current.DimensionalWeight = new ParcelWeight(){Weight = w, UnitOfMeasurement = u};
            return this;
        }
         public RatesArrayFluent DeliveryCommitment(DeliveryCommitment c) 
         {
            _current.DeliveryCommitment = c;
             return this;
         }

        public RatesArrayFluent CurrencyCode(string s) 
        {
            _current.CurrencyCode = s;
            return this;
        }
    }
}