using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Method;

namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    public class ShipmentFluent<T> where T : IShipment, new()
    {
        private T _shipment;

        public static implicit operator T(ShipmentFluent<T> s)
        {
            return s._shipment;
        }

        public static ShipmentFluent<T> Create()
        {
            var a = new ShipmentFluent<T>()
            {
                _shipment = new T()
            };
            return a;
        }

        public ShipmentFluent()
        {
            _shipment = new T();
        }

        public ShipmentFluent<T> ShipmentType(ShipmentType t)
        {
            _shipment.ShipmentType = t;
            return this;
        }
        public ShipmentFluent<T> TransactionId(string id)
        {
            _shipment.TransactionId = id;
            return this;
        }
        public ShipmentFluent<T> MinimalAddressValidation(string m)
        {
            _shipment.MinimalAddressValidation = m;
            return this;
        }
        public ShipmentFluent<T> ShipperRatePlan(string r)
        {
            _shipment.ShipperRatePlan = r;
            return this;
        }
        public ShipmentFluent<T> FromAddress( IAddress a)
        {
            _shipment.FromAddress = a;
            return this;
        }
        public ShipmentFluent<T> ToAddress(IAddress a)
        {
            _shipment.ToAddress = a;
            return this;
        }
        public ShipmentFluent<T> AltReturnAddress(IAddress a)
        {
            _shipment.AltReturnAddress = a;
            return this;
        }
        public ShipmentFluent<T> Parcel(IParcel p)
        {
            _shipment.Parcel = p;
            return this;
        }
        public ShipmentFluent<T> Rates(IEnumerable<IRates> r)
        {
            _shipment.Rates = r;
            return this;
        }
        public ShipmentFluent<T> AddRates(IRates r)
        {
            _shipment.AddRates(r);
            return this;
        }
        public ShipmentFluent<T> Documents(IEnumerable<IDocument> d)
        {
            _shipment.Documents = d;
            return this;
        }
        public ShipmentFluent<T> AddDocument(IDocument d)
        {
            _shipment.AddDocument(d);
            return this;
        }
        public ShipmentFluent<T> ShipmentOptions(IEnumerable<IShipmentOptions> o)
        {
            _shipment.ShipmentOptions = o;
            return this;
        }
        public ShipmentFluent<T> AddShipmentOptions(IShipmentOptions o)
        {
            _shipment.AddShipmentOptions(o);
            return this;
        }
        public ShipmentFluent<T> Customs(ICustoms c)
        {
            _shipment.Customs = c;
            return this;
        }

    }
}
