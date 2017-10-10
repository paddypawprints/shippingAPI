using Newtonsoft.Json;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Model;

namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    public class RatesArrayFluent<T> : List<T> where T : class, IRates, new()
    {

        protected T _current = null;

        public static RatesArrayFluent<T> Create()
        {
            return new RatesArrayFluent<T>();
        }

        public RatesArrayFluent<T> Add() 
        {
            Add(new T());
            _current = FindLast((x) => true);
            return this;
        }

        public RatesArrayFluent<T> First()
        {
            _current = Find((x) => true);
            return this;
        }

        public RatesArrayFluent<T> Next()
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

        public RatesArrayFluent<T> Carrier(Carrier c) 
        {
            _current.Carrier = c;
            return this;
        }

        public RatesArrayFluent<T> Service(Services s) 
        {
            _current.ServiceId = s;
            return this;
        }
        public RatesArrayFluent<T> ParcelType(ParcelType t) 
        {
            _current.ParcelType = t;
            return this;
        }

        public RatesArrayFluent<T> SpecialService<S>(SpecialServiceCodes c, decimal f, params IParameter[] parameters) where S:ISpecialServices, new()
        {
            var s = new S() { SpecialServiceId = c, Fee = f };

            foreach( var p in parameters )
            {
                s.AddParameter(p);
            }
            _current.AddSpecialservices(s);
            return this;
        }

        public RatesArrayFluent<T> SpecialService<S>(S s) where S : ISpecialServices, new()
        {
            _current.AddSpecialservices(s);
            return this;
        }

        public RatesArrayFluent<T> InductionPostalCode(string s) 
        {
            _current.InductionPostalCode = s;
            return this;
        }

        public RatesArrayFluent<T> DimensionalWeight<S>(decimal w, UnitOfWeight u) where S: IParcelWeight, new()
        {
            _current.DimensionalWeight = new S(){Weight = w, UnitOfMeasurement = u};
            return this;
        }
         public RatesArrayFluent<T> DeliveryCommitment(IDeliveryCommitment c) 
         {
            _current.DeliveryCommitment = c;
             return this;
         }

        public RatesArrayFluent<T> CurrencyCode(string s) 
        {
            _current.CurrencyCode = s;
            return this;
        }
        public RatesArrayFluent<T> Insurance(decimal amount)
        {
            return SpecialService<SpecialServices>(SpecialServiceCodes.Ins, 0M, new Parameter("INPUT_VALUE", amount.ToString()));
        }
    }
}